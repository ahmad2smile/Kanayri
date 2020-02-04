using System;
using System.Threading.Tasks;

namespace Kanayri.Domain
{
    public interface IEventRepository
    {
        Task<TAggregate> GetHydratedAggregate<TAggregate>(Guid id) where TAggregate: IAggregate, new();
        Task<bool> SaveAggregateEvent<TAggregate>(TAggregate aggregate, IEvent e) where TAggregate : IAggregate;
    }
}