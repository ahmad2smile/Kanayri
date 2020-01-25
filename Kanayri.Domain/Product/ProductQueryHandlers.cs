using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Queries;
using Kanayri.Persistence;
using Kanayri.Persistence.Models;

namespace Kanayri.Domain.Product
{
    public class ProductQueryHandlers : IQueryHandler<ProductGetQuery, ProductModel>
    {
        private readonly ApplicationContext _context;

        public ProductQueryHandlers(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductModel> Handle(ProductGetQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(request.Id); // Convert ValueTask to Task
        }
    }
}
