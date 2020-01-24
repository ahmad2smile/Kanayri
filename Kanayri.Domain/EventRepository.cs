using System;
using System.Linq;
using System.Threading.Tasks;
using Kanayri.Persistence;
using Kanayri.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Kanayri.Domain
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationContext _context;

        public EventRepository(ApplicationContext context)
        {
            _context = context;
        }

        private async Task<AggregateModel> AddAggregate<TAggregate>(Guid id)
        {
            var result = await _context.Aggregate.AddAsync(new AggregateModel
            {
                Id = id,
                Type = typeof(TAggregate).AssemblyQualifiedName,
                TotalEvents = 0
            });

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TAggregate> GetHydratedAggregate<TAggregate>(Guid id) where TAggregate : IAggregate, new()
        {
            var aggregate = new TAggregate { AggregateId = id };


            var events = await _context.Events.AsNoTracking()
                .Where(e => e.AggregateId == id)
                .ToListAsync();

            aggregate.Rehydrate(events.Select(e => DeserializeEvent(e.Type, e.Data)));

            return aggregate;
        }

        public async Task<TAggregate> GetAggregateByType<TAggregate>() where TAggregate : IAggregate, new()
        {
            var aggregateType = typeof(TAggregate).AssemblyQualifiedName;

            var agg = await _context.Aggregate.AsNoTracking()
                                     .Where(e => e.Type == aggregateType)
                                     .FirstOrDefaultAsync()
                                 ?? await AddAggregate<TAggregate>(Guid.NewGuid());

            return await GetHydratedAggregate<TAggregate>(agg.Id);
        }

        public async Task<bool> SaveAggregateEvent<TAggregate>(TAggregate aggregate, IEvent e) where TAggregate : IAggregate
        {
            var agg = await _context.Aggregate.FindAsync(aggregate.AggregateId)
                      ?? await AddAggregate<TAggregate>(aggregate.AggregateId);

            var eventModel = new EventModel
            {
                Data = SerializeEvent(e),
                Type = e.GetType().AssemblyQualifiedName,
                AggregateId = aggregate.AggregateId
            };

            await _context.Events.AddAsync(eventModel);

            agg.TotalEvents = aggregate.TotalEvents;

            _context.Aggregate.Update(agg);

            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception) // Possible concurrency exception from Aggregate Update 
            {
                Console.WriteLine(exception); // TODO: Move to Logger

                return false;
            }
        }

        private static string SerializeEvent(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private object DeserializeEvent(string typeName, string data)
        {
            var type = Type.GetType(typeName);

            return typeof(JsonConvert).GetMethod(nameof(JsonConvert.DeserializeObject))
                ?.MakeGenericMethod(type)
                .Invoke(this, new object[] { data });
        }
    }
}
