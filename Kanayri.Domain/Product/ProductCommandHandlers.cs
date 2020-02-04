using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Events;
using MediatR;

namespace Kanayri.Domain.Product
{
    public class ProductCommandHandlers : 
        ICommandHandler<ProductCreateCommand>,
        ICommandHandler<ProductChangePriceCommand>
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
            var aggregate = new Product();

            var productCreatedEvent =
                new ProductCreatedEvent(command.Id, command.Name, command.Price);

            aggregate.Handle(productCreatedEvent);

            await _repository.SaveAggregateEvent(aggregate, productCreatedEvent, cancellationToken);

            await _mediator.Publish(productCreatedEvent, cancellationToken);
        }

        public async Task Handle(ProductChangePriceCommand command, CancellationToken cancellationToken)
        {
            var aggregate = await _repository.GetHydratedAggregate<Product>(command.ProductId, cancellationToken);

            var priceChangedEvent = new ProductPriceChangedEvent(command.ProductId, command.Price);

            aggregate.Handle(priceChangedEvent);

            await _repository.SaveAggregateEvent(aggregate, priceChangedEvent, cancellationToken);

            await _mediator.Publish(priceChangedEvent, cancellationToken);
        }
    }
}
