namespace Kanayri.Domain
{
    public interface IEventSubscriber<in TEvent> where TEvent: IEvent
    {
        void Handle(TEvent e);
    }
}
