using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Events;
using Kanayri.Persistence;
using Kanayri.Persistence.Models;

namespace Kanayri.Domain.Product
{
    public class ProductCommandHandlers : ICommandHandler<ProductCreateCommand>
    {
        private readonly IEventRepository _repository;
        private readonly ApplicationContext _context;

        public ProductCommandHandlers(IEventRepository repository, ApplicationContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            // TODO: Get By Id
            var aggregate = await _repository.GetAggregateByType<Product>();

            var productCreatedEvent =
                new ProductCreatedEvent(command.Id, command.Name, command.Price);

            aggregate.Handle(productCreatedEvent);

            await _repository.SaveAggregateEvent(aggregate, productCreatedEvent);

            // TODO: Use Mapper
            var product = new ProductModel
            {
                Id = aggregate.Id,
                Name = aggregate.Name,
                Price = aggregate.Price
            };

            // TODO: Find better way to Update ReadModel
            await _context.Products.AddAsync(product, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
