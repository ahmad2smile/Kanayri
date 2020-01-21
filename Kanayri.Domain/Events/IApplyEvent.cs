namespace Kanayri.Domain.Events
{
    public interface IApplyEvent<in TEvent>
    {
        void Apply(TEvent e);
    }
}
