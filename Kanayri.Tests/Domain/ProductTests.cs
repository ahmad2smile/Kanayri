using System;
using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product;
using Kanayri.Domain.Product.Commands;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Kanayri.Tests.Domain
{
    public class ProductTests
    {
        [Fact]
        public async Task CreateProduct()
        {
            var (context, repository, mediator) = TestSetup.Init();

            var handler = new ProductCommandHandlers(repository, mediator);

            var command = new ProductCreateCommand
            {
                Id =  Guid.NewGuid(),
                Name = "New Product",
                Price = 500
            };

            await handler.Handle(command, new CancellationToken(false));

            var aggregates = await context.Aggregate.ToListAsync();
            var events = await context.Events.ToListAsync();

            Assert.Single(aggregates);
            Assert.Single(events);
        }
    }
}
