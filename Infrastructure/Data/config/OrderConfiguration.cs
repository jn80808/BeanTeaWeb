using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress , a => 
            {
                a.WithOwner();
            });

            builder.Navigation(a => a.ShipToAddress).IsRequired();

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(), 
                    o => (OrderStatus)  Enum.Parse(typeof(OrderStatus), o)
                );

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete
            (DeleteBehavior.Cascade);
        }
    }
}