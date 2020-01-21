using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kanayri.Persistence
{
    public static class PersistenceBootstrap
    {
        public static void ConfigurePersistence(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
