using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Domain.Dtos
{
    public class CreateProductItemDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Categories { get; set; }
    }
}
