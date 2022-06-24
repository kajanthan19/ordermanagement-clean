using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Migrations.Configuration
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order", "Order");
            builder.Property(x => x.OrderNo).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ProductName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.OrderDate).IsRequired(true);
            builder.Property(x => x.Total).HasColumnType("decimal(10,2)").IsRequired(true);
            builder.Property(x => x.Price).HasColumnType("decimal(10,2)").IsRequired(true);
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(10,2)").IsRequired(true);

            base.Configure(builder);
        }
    }
}
