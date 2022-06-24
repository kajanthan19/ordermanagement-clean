using OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Dtos
{
    public class PersonDto : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string Email { get; set; }
    }
}
