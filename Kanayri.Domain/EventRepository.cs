using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
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

        public async Task<IEnumerable<TEvent>> GetEventsOfType<TEvent>(Guid id, CancellationToken cancellationToken)
        {
            var events = await _context.Events.AsNoTracking()
                .Where(e => e.AggregateId == id && e.Type == typeof(TEvent).AssemblyQualifiedName)
                .ToListAsync(cancellationToken);

            return events.Select(e => DeserializeEvent<TEvent>(e.Data)); // TODO: Try before AsyncList
        }

        public async Task<TAggregate> GetHydratedAggregate<TAggregate>(Guid id, CancellationToken cancellationToken) where TAggregate : IAggregate, new()
        {
            var aggregate = new TAggregate { Id = id };


            var events = await _context.Events.AsNoTracking()
                .Where(e => e.AggregateId == id)
                .ToListAsync(cancellationToken);

            aggregate.Rehydrate(events.Select(e =>
            {
                var type = Type.GetType(e.Type);

                return GetType()
                    .GetMethod(nameof(DeserializeEvent), BindingFlags.NonPublic | BindingFlags.Static)?
                    .MakeGenericMethod(type)
                    .Invoke(this, new object[] { e.Data });
            }));

            return aggregate;
        }

        public async Task<bool> SaveAggregateEvent<TAggregate>(TAggregate aggregate, IEvent e, CancellationToken cancellationToken) where TAggregate : IAggregate
        {
            var agg = await _context.Aggregate.FindAsync(aggregate.Id)
                      ?? await AddAggregate<TAggregate>(aggregate.Id);

            var eventModel = new EventModel
            {
                Data = SerializeEvent(e),
                Type = e.GetType().AssemblyQualifiedName,
                AggregateId = aggregate.Id
            };

            await _context.Events.AddAsync(eventModel, cancellationToken);

            agg.TotalEvents = aggregate.TotalEvents;

            _context.Aggregate.Update(agg);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception exception) // Possible concurrency exception from Aggregate Update 
            {
                Console.WriteLine(exception); // TODO: Move to Logger

                return false;
            }
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

        private static string SerializeEvent(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static TEvent DeserializeEvent<TEvent>(string data)
        {
            return JsonConvert.DeserializeObject<TEvent>(data);
        }
    }
}
