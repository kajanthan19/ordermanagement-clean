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
    public class UpdatePersonCommand : IRequest<PersonDto>
    {
        public CreatePersonDto Model { get; }

        public int Id { get; }
        public UpdatePersonCommand(int id, CreatePersonDto model)
        {
            this.Model = model;
            this.Id = id;
        }
    }


    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonDto>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IValidator<CreatePersonDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonCommandHandler> _logger;

        public UpdatePersonCommandHandler(ILogger<UpdatePersonCommandHandler> logger, IUnitOfWork unitofwork,
            IValidator<CreatePersonDto> validator, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PersonDto> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            CreatePersonDto model = request.Model;
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

            var entity = await _unitofwork.PersonRepository.Get(id);
            entity.Name = model.Name;
            entity.Email = model.Email;
            _unitofwork.PersonRepository.Update(entity);
            await _unitofwork.Save();

            return _mapper.Map<PersonDto>(entity);
        }
    }
}
