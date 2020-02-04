using System;
using System.Collections.Generic;
using Kanayri.Domain.Product.Events;

namespace Kanayri.Domain.Product.Queries
{
    public class ProductPriceChangeQuery: IQuery<IEnumerable<ProductPriceChangedEvent>>
    {
        public ProductPriceChangeQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
