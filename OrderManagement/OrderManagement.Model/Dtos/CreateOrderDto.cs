using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Dtos
{
    public class CreateOrderDto
    {
        public string? OrderNo { get; set; }
        public string? ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public string CreatedBy { get; set; }
    }
}
