using System;
using MediatR;

namespace Kanayri.Domain
{
    public interface IEvent: INotification
    {
        DateTime FiredAt { get; }
    }
}
