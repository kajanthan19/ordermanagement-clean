using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using OrderManagement.API.UnitTests.Mocks;
using OrderManagement.Core.Handlers.Commands;
using OrderManagement.Core.Mapper;
using OrderManagement.Core.Validators;
using OrderManagement.Domain.Dtos;
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
   
    public class CreateProductItemCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private CreateProductItemDto _productitemDto;
        private readonly CreateProductItemCommandHandler _handler;
        private readonly IValidator<CreateProductItemDto> _validator;
        private readonly ILogger<CreateProductItemCommandHandler> _logger;

        public CreateProductItemCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            var mock = new Mock<ILogger<CreateProductItemCommandHandler>>();
            _logger = mock.Object;

            _validator = new CreateProductItemDtoValidator();

            _handler = new CreateProductItemCommandHandler(_logger, _mockUow.Object, _validator, _mapper);

            _productitemDto = new CreateProductItemDto
            {
                Name = "AC Parts",
                Description = "Ac parts sale",
                Categories="Electronics"
            };
        }


        [Fact]
        public async Task Valid_CreateProductItem_Added()
        {
            var result = await _handler.Handle(new CreateProductItemCommand(_productitemDto), CancellationToken.None);

            var orders = await _mockUow.Object.ProductItemRepository.GetAll();

            result.ShouldBeOfType<ProductItemDto>();

            orders.Count.ShouldBe(4);
        }


        [Fact]
        public async Task InValid_CreateProductItem_Added()
        {

            try
            {
                _productitemDto.Name = null;

                var result = await _handler.Handle(new CreateProductItemCommand(_productitemDto), CancellationToken.None);

                var orders = await _mockUow.Object.ProductItemRepository.GetAll();

                orders.Count.ShouldBe(3);

                result.ShouldBeOfType<OrderDto>();
            }
            catch (Exception)
            {
                var orders = await _mockUow.Object.ProductItemRepository.GetAll();
                orders.Count.ShouldBe(3);
            }

        }
    }
}
