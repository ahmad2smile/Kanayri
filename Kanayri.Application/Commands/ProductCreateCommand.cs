using Kanayri.Domain.Product;
using MediatR;

namespace Kanayri.Application.Commands
{
    public class ProductCreateCommand: IRequest<Product>
    {
        public string Name { get; set; }
    }
}
