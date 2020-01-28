using Kanayri.Domain;
using Kanayri.Persistence;
using MediatR;

namespace Kanayri.Tests
{
    public static class TestSetup
    {
        public static (ApplicationContext, EventRepository, Mediator) Init()
        {
            var context = new InMemoryDbFactory().GetDbContext();
            var repository = new EventRepository(context);

            var mediator = new MediatorFactory().GetMediator();

            return (context, repository, mediator);
        }
    }
}
