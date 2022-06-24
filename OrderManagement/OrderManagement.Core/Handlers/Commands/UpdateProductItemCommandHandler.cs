using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagement.Core.Exceptions;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Commands
{
    public class UpdateProductItemCommand : IRequest<ProductItemDto>
    {
        public CreateProductItemDto Model { get; }

        public int Id { get; }
        public UpdateProductItemCommand(int id, CreateProductItemDto model)
        {
            this.Model = model;
            this.Id = id;
        }
    }

    public class UpdateProductItemCommandHandler : IRequestHandler<UpdateProductItemCommand, ProductItemDto>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IValidator<CreateProductItemDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductItemCommandHandler> _logger;

        public UpdateProductItemCommandHandler(ILogger<UpdateProductItemCommandHandler> logger, IUnitOfWork unitofwork,
            IValidator<CreateProductItemDto> validator, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ProductItemDto> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
        {
            CreateProductItemDto model = request.Model;
            var id = request.Id;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                _logger.LogError($"Update Person Validation result: {result}");
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = await _unitofwork.ProductItemRepository.Get(id);
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Categories = model.Categories;
            _unitofwork.ProductItemRepository.Update(entity);
            await _unitofwork.Save();

            return _mapper.Map<ProductItemDto>(entity);
        }
    }

}
