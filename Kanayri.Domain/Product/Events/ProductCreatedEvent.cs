using System;

namespace Kanayri.Domain.Product.Events
{
    public class ProductCreatedEvent: IEvent
    {
        public ProductCreatedEvent(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
