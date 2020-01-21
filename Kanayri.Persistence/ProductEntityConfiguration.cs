using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kanayri.Persistence
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> builder)
        {
            builder.ToTable("products");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();
        }
    }
}
