using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class ProductItem: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
    }
}
