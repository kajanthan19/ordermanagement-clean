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

    public static class MockPersonRepository
    {
        public static Mock<IPersonRepository> GetPersonRepository()
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Test Vacation",
                    Email = "testvacation@yopmail.com",
                    CreatedBy = "",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },
                new Person
                {
                    Id = 2,
                    Name = "Test Sick",
                    Email = "testsick@yopmail.com",
                    CreatedBy = "",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                },
                new Person
                {
                    Id = 3,
                    Name = "Test Maternity",
                    Email = "testmaternity@yopmail.com",
                    CreatedBy = "",
                    IsDeleted= false,
                    CreatedOn = DateTime.Now,
                    LastModifiedOn = DateTime.Now
                }
            };

            var mockRepo = new Mock<IPersonRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(persons);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) => {
                var response = persons.Where(x => x.Id == id).Single();
                return response;
            });

            mockRepo.Setup(r => r.GetAll(It.IsAny<Expression<Func<Person,bool>>>())).ReturnsAsync((Expression<Func<Person, bool>> expressions) => {
                var response = persons.AsQueryable().Where(expressions).ToList();
                return response;
            });


            mockRepo.Setup(r => r.Add(It.IsAny<Person>())).ReturnsAsync((Person person) =>
            {
                persons.Add(person);
                return person;
            });

            return mockRepo;

        }
    }
}
