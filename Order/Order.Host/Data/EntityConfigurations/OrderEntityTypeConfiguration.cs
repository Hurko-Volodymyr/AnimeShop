using Microsoft.EntityFrameworkCore;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(h => h.Id);

            builder.Property(p => p.Id)
                .UseHiLo("order_hilo")
                .IsRequired();

            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.GameAccountId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnType("date");
            builder.Property(p => p.TotalSum).HasColumnType("money");
        }
    }
}
