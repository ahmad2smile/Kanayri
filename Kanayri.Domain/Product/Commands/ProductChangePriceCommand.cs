using System;

namespace Kanayri.Domain.Product.Commands
{
    public class ProductChangePriceCommand: ICommand
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
