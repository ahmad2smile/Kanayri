using System;
using Kanayri.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanayri.Persistence
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
                .HasData(
                    new ProductModel { Id = Guid.NewGuid(), Name = "iPhone 6 Plus", Price = 600 },
                    new ProductModel { Id = Guid.NewGuid(), Name = "iPhone 7 Plus", Price = 700 }
                );
        }
    }
}
