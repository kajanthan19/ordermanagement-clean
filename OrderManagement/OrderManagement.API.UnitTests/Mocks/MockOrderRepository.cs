using Moq;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.API.UnitTests.Mocks
{
    public static class MockOrderRepository
    {
        public static Mock<IOrderRepository> GetOrderRepository()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    OrderNo = "NN0012",
                    OrderDate = DateTime.Now,
                    ProductName = "Carrots",
                    Total = 50,
                    Price = 50,
                    TotalPrice= 2500,
                    CreatedBy = "yop@testemail.com",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },
                new Order
                {
                    Id = 2,
                    OrderNo = "NN0014",
                    OrderDate = DateTime.Now,
                    ProductName = "Cakes",
                    Total = 50,
                    Price = 50,
                    TotalPrice= 2500,
                    CreatedBy = "yop@testemail.com",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },
                new Order
                {
                    Id = 3,
                    OrderNo = "NN0015",
                    OrderDate = DateTime.Now,
                    ProductName = "Coffee",
                    Total = 50,
                    Price = 50,
                    TotalPrice= 2500,
                    CreatedBy = "yop@testemail.com",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }
            };

            var mockRepo = new Mock<IOrderRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(orders);


            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => {
                var response = orders.Where(x => x.Id == id).Single();
                return response;
            });

            mockRepo.Setup(r => r.GetAll(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync((Expression<Func<Order, bool>> expressions) => {
                var response = orders.AsQueryable().Where(expressions).ToList();
                return response;
            });


            mockRepo.Setup(r => r.Add(It.IsAny<Order>())).ReturnsAsync((Order order) =>
            {
                orders.Add(order);
                return order;
            });

            return mockRepo;

        }
    }
}
