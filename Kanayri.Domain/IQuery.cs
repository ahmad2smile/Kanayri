using MediatR;

namespace Kanayri.Domain
{
    public interface IQuery<out TModel> : IRequest<TModel>
    {
    }
}
