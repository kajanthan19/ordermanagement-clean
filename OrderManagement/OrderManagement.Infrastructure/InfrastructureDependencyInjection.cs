using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagement.Domain.IRepositories;
using OrderManagement.Domain.IUnitOfWork;
using OrderManagement.Infrastructure.Repository;
using OrderManagement.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureExtentionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderManagementContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
           

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
