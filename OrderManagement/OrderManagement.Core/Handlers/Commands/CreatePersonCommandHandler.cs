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
    public class CreatePersonCommand : IRequest<PersonDto>
    {
        public CreatePersonDto Model { get; }
        public CreatePersonCommand(CreatePersonDto model)
        {
            this.Model = model;
        }
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IValidator<CreatePersonDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePersonCommandHandler> _logger;

        public CreatePersonCommandHandler(ILogger<CreatePersonCommandHandler> logger, IUnitOfWork unitofwork,
            IValidator<CreatePersonDto> validator, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PersonDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            CreatePersonDto model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                _logger.LogError($"CreatePerson Validation result: {result}");
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = this._mapper.Map<Person>(model);

            await _unitofwork.PersonRepository.Add(entity);
            await _unitofwork.Save();

            return _mapper.Map<PersonDto>(entity);
        }
    }
}
