using Kanayri.Application.Commands.Products;
using Kanayri.Domain.Product;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Kanayri.Persistence;

namespace Kanayri.Application.Handlers.Products
{
    public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, ProductModel>
    {
        private readonly ApplicationContext _context;

        public ProductCreateHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductModel> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new ProductModel { Name = request.Name };

            var result = await _context.Products.AddAsync(newProduct, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }
    }
}
