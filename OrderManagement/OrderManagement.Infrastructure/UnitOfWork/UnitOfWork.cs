using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.IRepositories;
using OrderManagement.Domain.IUnitOfWork;
using OrderManagement.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementContext _context;
        public UnitOfWork(OrderManagementContext context,
            IOrderRepository orderRepository,
            IProductItemRepository productItemRepository,
            IPersonRepository personRepository)
        {
            _context = context;
            this.OrderRepository = orderRepository;
            this.ProductItemRepository = productItemRepository;
            this.PersonRepository = personRepository;
        }

        public IOrderRepository OrderRepository { get; set; }

        public IPersonRepository PersonRepository { get; set; }

        public IProductItemRepository ProductItemRepository { get; set; }

        public async Task<int> Save()
        {
            try
            {

                var modifiedEntries = _context.ChangeTracker.Entries();

                foreach (var entry in modifiedEntries)
                {
                    var entity = entry.Entity as ITrackableEntity;
                    if (entity != null)
                    {

                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;

                        }
                        entity.LastModifiedOn = DateTime.UtcNow;
                    }

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                //We dont do anything about this exception
            }

            int affectedRows = await this._context.SaveChangesAsync();
            return affectedRows;
        }
    }
}
