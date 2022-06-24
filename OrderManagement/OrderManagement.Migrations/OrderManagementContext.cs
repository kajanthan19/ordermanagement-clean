using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Migrations.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Migrations
{
    public partial class OrderManagementContext : DbContext
    {
        public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Configure(builder);
        }


        private static void Configure(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductItemConfiguration());
            builder.ApplyConfiguration(new PersonConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());

        }
    }
}
