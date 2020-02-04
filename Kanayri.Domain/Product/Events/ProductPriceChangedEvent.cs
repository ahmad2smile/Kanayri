using System;

namespace Kanayri.Domain.Product.Events
{
    public class ProductPriceChangedEvent: Event
    {
        public ProductPriceChangedEvent(Guid productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }

        public Guid ProductId { get; }
        public decimal Price { get; }
    }
}
