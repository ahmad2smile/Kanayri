using MediatR;
using Kanayri.Domain.Product;

namespace Kanayri.Application.Commands.Products
{
    public class ProductCreateCommand : IRequest<Product>
    {
        public string Name { get; set; }
    }
}
