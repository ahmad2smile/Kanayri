using System;
using MediatR;

namespace Kanayri.Domain.Product.Queries
{
    public class ProductGetQuery: IRequest<Persistence.ProductModel>
    {
        public Guid Id { get; set; }

        public ProductGetQuery(Guid id)
        {
            Id = id;
        }
    }
}
