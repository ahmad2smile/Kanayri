using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Events;
using MediatR;

namespace Kanayri.Domain.Product
{
    public class ProductCommandHandlers : ICommandHandler<ProductCreateCommand>
    {
        private readonly IEventRepository _repository;
        private readonly IMediator _mediator;

        public ProductCommandHandlers(IEventRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(ProductCreateCommand command, CancellationToken cancellationToken)
        {
            // TODO: Get By Id
            var aggregate = await _repository.GetAggregateByType<Product>();

            var productCreatedEvent =
                new ProductCreatedEvent(command.Id, command.Name, command.Price);

            aggregate.Handle(productCreatedEvent);

            await _repository.SaveAggregateEvent(aggregate, productCreatedEvent);

            await _mediator.Publish(productCreatedEvent, cancellationToken);
        }
    }
}
