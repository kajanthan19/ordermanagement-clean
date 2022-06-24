using Moq;
using OrderManagement.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.API.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            var mockPersonRepo = MockPersonRepository.GetPersonRepository();
            var mockOrderRepo = MockOrderRepository.GetOrderRepository();
            var mockProductItemRepo = MockProductItemRepository.GetProductItemRepository();

            mockUow.Setup(r => r.PersonRepository).Returns(mockPersonRepo.Object);
            mockUow.Setup(r => r.OrderRepository).Returns(mockOrderRepo.Object);
            mockUow.Setup(r => r.ProductItemRepository).Returns(mockProductItemRepo.Object);

            return mockUow;
        }
    }
}
