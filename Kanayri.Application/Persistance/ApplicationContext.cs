using Kanayri.Domain.Product;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kanayri.Application.Persistance
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product { Id = Guid.NewGuid(), Name = "iPhone 6 Plus"},
                    new Product { Id = Guid.NewGuid(), Name = "iPhone 7 Plus"}
                );
        }
    }
}
