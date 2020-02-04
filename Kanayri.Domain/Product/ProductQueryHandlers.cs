using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Events;
using Kanayri.Domain.Product.Queries;
using Kanayri.Persistence;
using Kanayri.Persistence.Models;

namespace Kanayri.Domain.Product
{
    public class ProductQueryHandlers : 
        IQueryHandler<ProductGetQuery, ProductModel>,
        IQueryHandler<ProductPriceChangeQuery, IEnumerable<ProductPriceChangedEvent>>
    {
        private readonly ApplicationContext _context;
        private readonly IEventRepository _eventRepository;

        public ProductQueryHandlers(ApplicationContext context, IEventRepository eventRepository)
        {
            _context = context;
            _eventRepository = eventRepository;
        }

        public async Task<ProductModel> Handle(ProductGetQuery query, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(query.Id); // Convert ValueTask to Task
        }

        public Task<IEnumerable<ProductPriceChangedEvent>> Handle(ProductPriceChangeQuery query, CancellationToken cancellationToken)
        {
            return _eventRepository.GetEventsOfType<ProductPriceChangedEvent>(query.Id, cancellationToken);
        }
    }
}
