using System;
using System.Collections;
using System.Collections.Generic;
using Kanayri.Domain.Events;

namespace Kanayri.Domain
{
    public abstract class Aggregate
    {
        protected Guid Id { get; set; }
        protected int Version { get; private set; }

        private readonly IList<IEvent> _uncommittedEvents = new List<IEvent>();

        public void ApplyEvents(IEnumerable events)
        {
            foreach (var e in events)
            {
                GetType().GetMethod("ApplyOneEvent")
                    ?.MakeGenericMethod(e.GetType())
                    .Invoke(this, new[] {e});
            }
        }

        public void ApplyEvent<TEvent>(TEvent e)
        {
            if (!(this is IApplyEvent<TEvent> applier))
            {
                throw new InvalidOperationException(
                    $"Aggregate {GetType().Name} does not know how to apply event {e.GetType().Name}");
            }

            applier.Apply(e);

            Version++;
        }
    }
}
