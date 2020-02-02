using System;
using System.Collections;
using System.Reflection;

namespace Kanayri.Domain
{
    public abstract class Aggregate : IAggregate
    {
        public Guid AggregateId { get; set; }

        public Guid Id { get; protected set; }
        public int TotalEvents { get; set; }

        public void Rehydrate(IEnumerable events)
        {
            try
            {
                var applyMethod = GetType().BaseType?
                    .GetMethod(nameof(ApplyEvent), BindingFlags.NonPublic | BindingFlags.Instance);

                if (applyMethod == null)
                {
                    throw new InvalidOperationException($"ApplyEvent method not found in base class of {GetType().Name}");
                }

                foreach (var e in events)
                {
                    applyMethod
                        .MakeGenericMethod(e.GetType())
                        .Invoke(this, new[] {e});
                }
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                {
                    throw e.InnerException;
                }
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
