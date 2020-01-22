using System;
using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Events;

namespace Kanayri.Domain.Product
{
    public class Product: Aggregate, IEventHandler<ProductCreatedEvent>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Task Handle(ProductCreatedEvent e, CancellationToken cancellationToken)
        {
            Id = e.Id;
            Name = e.Name;
            Price = e.Price;

            return Task.CompletedTask;
        }
    }
}
