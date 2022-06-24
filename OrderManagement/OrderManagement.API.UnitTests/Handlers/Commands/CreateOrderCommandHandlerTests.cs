using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using OrderManagement.API.UnitTests.Mocks;
using OrderManagement.Core.Handlers.Commands;
using OrderManagement.Core.Mapper;
using OrderManagement.Core.Validators;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.IUnitOfWork;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagement.API.UnitTests.Handlers.Commands
{

    public class CreateOrderCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private CreateOrderDto _orderDto;
        private readonly CreateOrderCommandHandler _handler;
        private readonly IValidator<CreateOrderDto> _validator;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            var mock = new Mock<ILogger<CreateOrderCommandHandler>>();
            _logger = mock.Object;

            _validator = new CreateOrderDtoValidator();

            _handler = new CreateOrderCommandHandler(_logger, _mockUow.Object, _validator, _mapper);

            _orderDto = new CreateOrderDto
            {
                OrderNo = "NN0011",
                OrderDate = DateTime.Now,
                ProductName = "AC Parts",
                Total = 50,
                Price = 50,
                TotalPrice= 2500,
                CreatedBy = "yop@testemail.com"
            };
        }

        [Fact]
        public async Task Valid_CreateOrder_Added()
        {
            var result = await _handler.Handle(new CreateOrderCommand(_orderDto), CancellationToken.None);

            var orders = await _mockUow.Object.OrderRepository.GetAll();

            result.ShouldBeOfType<OrderDto>();

            orders.Count.ShouldBe(4);
        }


        [Fact]
        public async Task InValid_CreateOrder_Added()
        {

            try
            {
                _orderDto.CreatedBy = null;

                var result = await _handler.Handle(new CreateOrderCommand(_orderDto), CancellationToken.None);

                var orders = await _mockUow.Object.OrderRepository.GetAll();

                orders.Count.ShouldBe(3);

                result.ShouldBeOfType<OrderDto>();
            }
            catch (Exception)
            {
                var orders = await _mockUow.Object.OrderRepository.GetAll();
                orders.Count.ShouldBe(3);
            }

        }
    }
}
