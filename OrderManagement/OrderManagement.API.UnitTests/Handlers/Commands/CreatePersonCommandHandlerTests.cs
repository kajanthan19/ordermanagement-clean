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
    public class CreatePersonCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private  CreatePersonDto _personDto;
        private readonly CreatePersonCommandHandler _handler;
        private readonly IValidator<CreatePersonDto> _validator;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        public CreatePersonCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper() ;

            var mock = new Mock<ILogger<CreatePersonCommandHandler>>();
            _logger = mock.Object;

            _validator = new CreatePersonDtoValidator();

            _handler = new CreatePersonCommandHandler(_logger, _mockUow.Object, _validator, _mapper);

            _personDto = new CreatePersonDto
            {
                Name = "Test DTO",
                Email="yop@testemail.com"
            };
        }



        [Fact]
        public async Task Valid_CreatePerson_Added()
        {
            var result = await _handler.Handle(new CreatePersonCommand(_personDto), CancellationToken.None);

            var orders = await _mockUow.Object.PersonRepository.GetAll();

            result.ShouldBeOfType<PersonDto>();

            orders.Count.ShouldBe(4);
        }


        [Fact]
        public async Task InValid_CreateOrder_Added()
        {

            try
            {
                _personDto.Name = null;

                var result = await _handler.Handle(new CreatePersonCommand(_personDto), CancellationToken.None);

                var orders = await _mockUow.Object.PersonRepository.GetAll();

                orders.Count.ShouldBe(3);

                result.ShouldBeOfType<PersonDto>();
            }
            catch (Exception)
            {
                var orders = await _mockUow.Object.PersonRepository.GetAll();
                orders.Count.ShouldBe(3);
            }

        }
    }
}
