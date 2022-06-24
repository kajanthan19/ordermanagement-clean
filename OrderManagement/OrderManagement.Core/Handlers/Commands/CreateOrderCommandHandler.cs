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
    public class CreateOrderCommand : IRequest<OrderDto>
    {
        public CreateOrderDto Model { get; }
        public CreateOrderCommand(CreateOrderDto model)
        {
            this.Model = model;
        }
    }


    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IValidator<CreateOrderDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(ILogger<CreateOrderCommandHandler> logger, IUnitOfWork unitofwork,
            IValidator<CreateOrderDto> validator, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            CreateOrderDto model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                _logger.LogError($"Create Order Validation result: {result}");
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = this._mapper.Map<Order>(model);

            await _unitofwork.OrderRepository.Add(entity);
            await _unitofwork.Save();

            return _mapper.Map<OrderDto>(entity);
        }
    }
}
