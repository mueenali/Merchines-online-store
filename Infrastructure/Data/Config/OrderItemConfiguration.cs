using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrdered, io =>
            {
                io.WithOwner();
                io.Property(p => p.ProductId).IsRequired();
                io.Property(p => p.ProductName).IsRequired();
                io.Property(p => p.PictureUrl).IsRequired();
            });
            
            builder.Property(i => i.Price).HasColumnType("decimal(18.2)");
        }
    }
}