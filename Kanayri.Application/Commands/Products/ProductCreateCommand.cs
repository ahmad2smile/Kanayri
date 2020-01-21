using MediatR;
using Kanayri.Persistence;

namespace Kanayri.Application.Commands.Products
{
    public class ProductCreateCommand : IRequest<ProductModel>
    {
        public string Name { get; set; }
    }
}
