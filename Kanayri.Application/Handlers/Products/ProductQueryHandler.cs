using Kanayri.Application.Persistance;
using Kanayri.Application.Queries.Products;
using Kanayri.Domain.Product;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanayri.Application.Handlers.Products
{
    public class ProductQueryHandler : IRequestHandler<ProductQuery, Product>
    {
        private readonly ApplicationContext _context;

        public ProductQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(ProductQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(request.Id); // Convert ValueTask to Task
        }
    }
}
