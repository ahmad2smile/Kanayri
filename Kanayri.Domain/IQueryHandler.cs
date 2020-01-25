using MediatR;

namespace Kanayri.Domain
{
    public interface IQueryHandler<in TQuery, TModel> : IRequestHandler<TQuery, TModel> where TQuery : IQuery<TModel>
    {
    }
}
