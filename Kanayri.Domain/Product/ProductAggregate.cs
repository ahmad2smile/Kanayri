using System;
using System.Threading;
using System.Threading.Tasks;
using Kanayri.Domain.Product.Queries;
using MediatR;

namespace Kanayri.Domain.Product
{
    public class ProductAggregate: Aggregate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
