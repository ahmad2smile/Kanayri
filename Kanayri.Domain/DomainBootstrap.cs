using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kanayri.Domain
{
    public static class DomainBootstrap
    {
        public static void ConfigureDomain(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DomainBootstrap));
            services.AddScoped<IEventRepository, EventRepository>();
        }
    }
}
