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
    public abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase>
     where TBase : BaseEntity<int>
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            //Base Configuration
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.LastModifiedOn).IsRequired(false);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.LastModifiedBy).IsRequired(false).HasMaxLength(200); ;
        }
    }
}
