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

    public static class MockProductItemRepository
    {
        public static Mock<IProductItemRepository> GetProductItemRepository()
        {
            var items = new List<ProductItem>
            {
                new ProductItem
                {
                    Id = 1,
                    Name = "Carrots",
                    Description="Vegetables",
                    Categories = "grocesory",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },
                new ProductItem
                {
                    Id = 2,
                    Name = "Cakes",
                    Description="eat item",
                    Categories = "Shortties",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },
                new ProductItem
                {
                    Id = 3,
                    Name = "Coffee", 
                    Description="drink",
                    Categories = "srink  ",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }
            };

            var mockRepo = new Mock<IProductItemRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(items);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => {
                var response = items.Where(x => x.Id == id).Single();
                return response;
            });

            mockRepo.Setup(r => r.GetAll(It.IsAny<Expression<Func<ProductItem, bool>>>())).ReturnsAsync((Expression<Func<ProductItem, bool>> expressions) => {
                var response = items.AsQueryable().Where(expressions).ToList();
                return response;
            });

            mockRepo.Setup(r => r.Add(It.IsAny<ProductItem>())).ReturnsAsync((ProductItem item) =>
            {
                items.Add(item);
                return item;
            });

            return mockRepo;

        }
    }
}
