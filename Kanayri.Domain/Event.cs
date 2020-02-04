using System;

namespace Kanayri.Domain
{
    public class Event: IEvent
    {
        public DateTime FiredAt { get; } = DateTime.UtcNow;
    }
}
