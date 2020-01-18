using Kanayri.Application.Commands.Products;
using Kanayri.Application.Persistance;
using Kanayri.Domain.Product;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanayri.Application.Handlers.Products
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly ApplicationContext _context;

        public ProductCreateHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product { Name = request.Name };

            var result = await _context.Products.AddAsync(newProduct);

            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
