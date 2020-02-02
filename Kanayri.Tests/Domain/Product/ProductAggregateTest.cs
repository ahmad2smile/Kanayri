using System;
using Kanayri.Domain.Product.Events;
using Xunit;

namespace Kanayri.Tests.Domain.Product
{
    public class ProductAggregateTest
    {
        [Fact]
        public void HandleCreatedProductEventOnAgg()
        {
            var id = Guid.NewGuid();
            const string name = "Test Product Name";
            const int price = 400;

            var productCreatedEvent = new ProductCreatedEvent(id, name, price);

            var agg = new Kanayri.Domain.Product.Product();

            agg.Handle(productCreatedEvent);

            Assert.Equal(id, agg.Id);
            Assert.Equal(name, agg.Name);
            Assert.Equal(price, agg.Price);
        }
    }
}
