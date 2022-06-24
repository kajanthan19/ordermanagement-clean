using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrderManagement.Core.Mapper;
using OrderManagement.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core
{
    public static class CoreDependencyInjection
    {
        public static IServiceCollection AddCoreExtentionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetEntryAssembly());

            services.AddSingleton(provider => new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); }).CreateMapper());

            return services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
