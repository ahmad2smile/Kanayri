using System;
using Kanayri.Persistence.Models;
using MediatR;

namespace Kanayri.Domain.Product.Queries
{
    public class ProductGetQuery: IRequest<ProductModel>
    {
        public Guid Id { get; set; }

        public ProductGetQuery(Guid id)
        {
            Id = id;
        }
    }
}
