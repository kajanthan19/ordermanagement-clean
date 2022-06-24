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
    public class ProductItemConfiguration : BaseEntityConfiguration<ProductItem>
    {
        public override void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.ToTable("ProductItem", "Product");
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.Categories).HasMaxLength(150);
            base.Configure(builder);
        }
    }
}
