using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kanayri.Persistence.Models
{
    public class EventModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }

        public Guid AggregateId { get; set; }
        public AggregateModel Aggregate { get; set; }
    }
}
