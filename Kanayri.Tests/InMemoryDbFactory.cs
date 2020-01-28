using Kanayri.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Kanayri.Tests
{
    public class InMemoryDbFactory
    {
        private readonly SqliteConnection _connection;

        public InMemoryDbFactory()
        {
            /*
             * For many test purposes these differences will not matter.
             * However, if you want to test against something
             * that behaves more like a true relational database,
             * then consider using SQLite in-memory mode.
             * https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
             */

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public ApplicationContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(_connection)
                .Options;
            var dbContext = new ApplicationContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
