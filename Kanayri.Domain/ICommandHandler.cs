using MediatR;

namespace Kanayri.Domain
{
    public interface ICommandHandler<in TCommand>: INotificationHandler<TCommand> where TCommand: INotification
    {
    }
}
