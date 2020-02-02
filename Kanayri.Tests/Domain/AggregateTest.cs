using System;
using System.Collections.Generic;
using System.Linq;
using Kanayri.Domain;
using Xunit;

namespace Kanayri.Tests.Domain
{
    public class MockEvent : IEvent
    {
        public DateTime FiredAt { get; } = DateTime.UtcNow;
    }

    public class MockAggregate : Aggregate
    {
    }

    public class MockAggregateOfAggregate : MockAggregate
    {
    }

    public class MockAggregateWithHandler : Aggregate, IEventSubscriber<IEvent>
    {
        public void Handle(IEvent e)
        {
        }
    }

    public class AggregateTest
    {
        private static IEnumerable<IEvent> MockEvents => new List<IEvent>
        {
            new MockEvent(),
            new MockEvent(),
            new MockEvent(),
        };

        [Fact]
        public void ThrowsOnNoHandlerForEvent()
        {
            var mockAggregate = new MockAggregate();

            Assert.Throws<InvalidOperationException>(() => mockAggregate.Rehydrate(MockEvents));
        }

        
        [Fact]
        public void ThrowsOnNoHandlerForEventWithMessage()
        {
            var mockAggregate = new MockAggregate();
            var message = "";

            try
            {
                mockAggregate.Rehydrate(MockEvents);
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            Assert.Contains("does not know how to Handle event", message);
        }

        [Fact]
        public void ThrowsWhenInheritanceDepthMoreThanOne()
        {

            var aggregateOfAggregate = new MockAggregateOfAggregate();

            var message = "";

            try
            {
                aggregateOfAggregate.Rehydrate(MockEvents);
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            Assert.Contains("ApplyEvent method not found in base class of", message);
        }

        [Fact]
        public void TotalEventsShouldBeEqualOnRehydrate()
        {
            var aggregate = new MockAggregateWithHandler();

            aggregate.Rehydrate(MockEvents);

            Assert.Equal(MockEvents.Count(), aggregate.TotalEvents);
        }
    }
}
