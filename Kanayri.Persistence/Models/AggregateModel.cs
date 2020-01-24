using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kanayri.Persistence.Models
{
    public class AggregateModel
    {
        public Guid Id { get; set; }
        public int TotalEvents { get; set; }
        public string Type { get; set; }

        [ConcurrencyCheck]
        [Timestamp]
        public byte[] Version { get; set; }

        public virtual ICollection<EventModel> Events { get; set; }
    }
}
