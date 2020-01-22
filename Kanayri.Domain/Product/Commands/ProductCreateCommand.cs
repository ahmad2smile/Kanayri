using System;

namespace Kanayri.Domain.Product.Commands
{
    public class ProductCreateCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
