using Kanayri.Domain.Product;
using MediatR;
using System;

namespace Kanayri.Application.Queries.Products
{
    public class ProductQuery: IRequest<Product>
    {
        public Guid Id { get; }

        public ProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
