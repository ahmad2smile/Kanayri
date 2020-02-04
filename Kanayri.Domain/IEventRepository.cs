using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kanayri.Domain
{
    public interface IEventRepository
    {
        Task<IEnumerable<TEvent>> GetEventsOfType<TEvent>(Guid id, CancellationToken cancellationToken);
        Task<TAggregate> GetHydratedAggregate<TAggregate>(Guid id, CancellationToken cancellationToken) where TAggregate: IAggregate, new();
        Task<bool> SaveAggregateEvent<TAggregate>(TAggregate aggregate, IEvent e, CancellationToken cancellationToken) where TAggregate : IAggregate;
    }
}