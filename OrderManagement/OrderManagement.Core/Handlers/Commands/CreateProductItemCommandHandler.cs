using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagement.Core.Exceptions;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Commands
{
    public class CreateProductItemCommand : IRequest<ProductItemDto>
    {
        public CreateProductItemDto Model { get; }
        public CreateProductItemCommand(CreateProductItemDto model)
        {
            this.Model = model;
        }
    }

    public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, ProductItemDto>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IValidator<CreateProductItemDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductItemCommandHandler> _logger;

        public CreateProductItemCommandHandler(ILogger<CreateProductItemCommandHandler> logger, IUnitOfWork unitofwork, 
            IValidator<CreateProductItemDto> validator, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ProductItemDto> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
        {
            CreateProductItemDto model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                _logger.LogError($"CreateProductItem Validation result: {result}");
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = this._mapper.Map<ProductItem>(model);

            await _unitofwork.ProductItemRepository.Add(entity);
            await _unitofwork.Save();

            return _mapper.Map<ProductItemDto>(entity);
        }
    }
}
