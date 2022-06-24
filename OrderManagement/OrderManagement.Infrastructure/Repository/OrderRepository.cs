using OrderManagement.Domain.Entities;
using OrderManagement.Domain.IRepositories;
using OrderManagement.Infrastructure.GenericRepository;
using OrderManagement.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderManagementContext context) : base(context)
        {
        }
    }
}
