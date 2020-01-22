using System;

namespace Kanayri.Domain
{
    public abstract class Aggregate
    {
        protected Guid Id { get; set; }
        protected int Version { get; set; }
    }
}
