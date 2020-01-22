using MediatR;

namespace Kanayri.Domain
{
    public interface IEventHandler<in TEvent>: INotificationHandler<TEvent> where TEvent : INotification
    {
    }
}
