using System;

namespace Kanayri.Domain.Product.Events
{
    public class ProductCreatedEvent : IEvent
    {
        public ProductCreatedEvent(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }
    }
}
