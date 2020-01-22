using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Events;
using Kanayri.Persistence;
using Kanayri.Persistence.Models;
using MediatR;

namespace Kanayri.Domain.Product
{
    public class ProductCommandHandlers: ICommandHandler<ProductCreateCommand>
    {
        private readonly ApplicationContext _context;
        private readonly IMediator _mediator;

        public ProductCommandHandlers(ApplicationContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }
        public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            // TODO: Do Event Sourcing with saving the Event here only
            await _context.Products.AddAsync(new ProductModel
            {
                Id = command.Id,
                Name = command.Name,
                Price = command.Price
            }, cancellationToken);

            await _mediator.Publish(new ProductCreatedEvent(command.Id, command.Name, command.Price), cancellationToken);
        }
    }
}
