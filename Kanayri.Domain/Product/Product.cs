using Kanayri.Domain.Product.Events;

namespace Kanayri.Domain.Product
{
    public class Product: 
        Aggregate,
        IEventSubscriber<ProductCreatedEvent>,
        IEventSubscriber<ProductPriceChangedEvent>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public void Handle(ProductCreatedEvent e)
        {
            Id = e.Id;
            Name = e.Name;
            Price = e.Price;
        }

        public void Handle(ProductPriceChangedEvent e)
        {
            Price = e.Price;
        }
    }
}
