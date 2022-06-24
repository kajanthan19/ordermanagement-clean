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
    public class UpdateOrderCommand : IRequest<OrderDto>
    {
        public CreateOrderDto Model { get; }

        public int Id { get; }
        public UpdateOrderCommand(int id, CreateOrderDto model)
        {
            this.Model = model;
            this.Id = id;
        }
    }


    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IValidator<CreateOrderDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger, IUnitOfWork unitofwork,
            IValidator<CreateOrderDto> validator, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            CreateOrderDto model = request.Model;
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

            var entity = await _unitofwork.OrderRepository.Get(id);
            entity.OrderNo = model.OrderNo;
            entity.ProductName = model.ProductName;
            entity.OrderDate = model.OrderDate;
            entity.Total = model.Total;
            entity.Price = model.Price;
            entity.TotalPrice = model.TotalPrice;
            entity.CreatedBy = model.CreatedBy;
            _unitofwork.OrderRepository.Update(entity);
            await _unitofwork.Save();

            return _mapper.Map<OrderDto>(entity);
        }
    }
}
