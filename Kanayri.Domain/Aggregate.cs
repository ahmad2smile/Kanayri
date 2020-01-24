using System;
using System.Collections;

namespace Kanayri.Domain
{
    public abstract class Aggregate : IAggregate
    {
        public Guid AggregateId { get; set; }

        public Guid Id { get; protected set; }
        public int TotalEvents { get; set; }

        public void Rehydrate(IEnumerable events)
        {
            foreach (var e in events)
            {
                var method = GetType().GetMethod(nameof(ApplyEvent));

                if (method == null)
                {
                    throw new InvalidOperationException( $"Aggregate {GetType().Name} doesn't have Handler for event {e.GetType().Name}");
                }

                method.MakeGenericMethod(e.GetType())
                    .Invoke(this, new[] {e});
            }
        }

        private void ApplyEvent<TEvent>(TEvent e) where TEvent : IEvent
        {
            if (!(this is IEventSubscriber<TEvent> aggregate))
            {
                throw new InvalidOperationException(
                    $"Aggregate {GetType().Name} does not know how to Handle event {e.GetType().Name}");
            }

            aggregate.Handle(e);

            TotalEvents++;
        }
    }
}
