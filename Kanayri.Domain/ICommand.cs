using MediatR;

namespace Kanayri.Domain
{
    // Only required to run Handler (abstraction for manual Reflection based execution of Handlers)
    public interface ICommand: INotification
    {
    }
}
