using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, a =>
                {
                     a.WithOwner();
                     a.Property(x => x.FirstName).IsRequired();
                     a.Property(x => x.LastName).IsRequired();
                     a.Property(x => x.City).IsRequired();
                     a.Property(x => x.State).IsRequired();
                     a.Property(x => x.Street).IsRequired();
                     a.Property(x => x.ZipCode).IsRequired();
                });
            
            builder.Property(s => s.OrderStatus)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o)
                );
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}