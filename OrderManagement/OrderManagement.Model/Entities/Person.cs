using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Entities
{
    public class Person : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string Email { get; set; }
    }
}
