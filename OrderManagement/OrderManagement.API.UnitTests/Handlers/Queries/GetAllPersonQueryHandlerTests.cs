using AutoMapper;
using Moq;
using OrderManagement.API.UnitTests.Mocks;
using OrderManagement.Core.Handlers.Queries;
using OrderManagement.Core.Mapper;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.IRepositories;
using OrderManagement.Domain.IUnitOfWork;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderManagement.API.UnitTests.Handlers.Queries
{
    public class GetAllPersonQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        public GetAllPersonQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllPersonsTest()
        {
            var handler = new GetAllPersonQueryHandler(_mockUow.Object, _mapper);

            var result = await handler.Handle(new GetAllPersonQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<PersonDto>>();

            result.Count.ShouldBe(3);
        }
    }
}
