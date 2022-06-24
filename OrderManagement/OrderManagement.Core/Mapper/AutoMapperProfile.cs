using AutoMapper;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            
            this.CreateMap<CreateProductItemDto, ProductItem>().ReverseMap();
            this.CreateMap<ProductItem, ProductItemDto>().ReverseMap();

            this.CreateMap<CreatePersonDto, Person>().ReverseMap();
            this.CreateMap<Person, PersonDto>().ReverseMap();

            this.CreateMap<CreateOrderDto, Order>().ReverseMap();
            this.CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
