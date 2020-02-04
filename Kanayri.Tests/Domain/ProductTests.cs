﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product;
using Kanayri.Domain.Product.Commands;
using Kanayri.Domain.Product.Queries;
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
                Id = Guid.NewGuid(),
                Name = "New Product",
                Price = 500
            };


            await handler.Handle(command, new CancellationToken(false));

            var aggregates = await context.Aggregate.ToListAsync();
            var events = await context.Events.ToListAsync();

            Assert.Single(aggregates);
            Assert.Single(events);
        }

        [Fact]
        public async Task QueryProduct()
        {
            var (context, _, _) = TestSetup.Init();

            var handler = new ProductQueryHandlers(context);

            var firstProduct = await context.Products.FirstOrDefaultAsync();

            var query = new ProductGetQuery(firstProduct.Id);

            var product = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(firstProduct.Id, product.Id);
        }

        [Fact]
        public async Task ChangePrice()
        {
            var (_, repository, mediator) = TestSetup.Init();

            var handler = new ProductCommandHandlers(repository, mediator);

            var id = Guid.NewGuid();

            var productCreateCommand = new ProductCreateCommand
            {
                Id = id,
                Name = "mock name",
                Price = 500
            };

            await handler.Handle(productCreateCommand, CancellationToken.None);

            const int newPrice = 600;
            var priceChangeCommand = new ProductChangePriceCommand
            {
                ProductId = id,
                Price = newPrice
            };

            await handler.Handle(priceChangeCommand, CancellationToken.None);

            var agg = await repository.GetHydratedAggregate<Kanayri.Domain.Product.Product>(id);

            Assert.Equal(newPrice, agg.Price);
        }
    }
}
