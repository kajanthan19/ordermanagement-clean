using FluentValidation;
using OrderManagement.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Validators
{

    public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
    {
        public CreatePersonDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");

        }
    }
}
