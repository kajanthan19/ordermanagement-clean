using FluentValidation;
using OrderManagement.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{
 
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.OrderNo).NotEmpty().WithMessage("Order No is required");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Product Name is required");
            RuleFor(x => x.Total).NotEmpty().WithMessage("Total is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("Total Price is required");
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order Date is required");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("Created By is required");

        }
    }
}
