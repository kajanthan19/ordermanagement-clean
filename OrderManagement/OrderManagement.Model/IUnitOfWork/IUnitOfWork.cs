using OrderManagement.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IOrderRepository OrderRepository { get; }
        IPersonRepository PersonRepository { get; }
        IProductItemRepository ProductItemRepository { get; }
        Task<int> Save();
    }
}
