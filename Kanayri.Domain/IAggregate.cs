using System;
using System.Collections;

namespace Kanayri.Domain
{
    public interface IAggregate
    {
        Guid Id { get; set; }
        int TotalEvents { get; set; }
        
        void Rehydrate(IEnumerable events);
    }
}
