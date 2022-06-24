using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Commands
{
    public class DeletePersonCommand : IRequest<bool>
    {
        public int Id { get; }
        public DeletePersonCommand(int id)
        {
            this.Id = id;
        }
    }

    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePersonCommandHandler> _logger;

        public DeletePersonCommandHandler(ILogger<DeletePersonCommandHandler> logger, IUnitOfWork unitofwork,
         IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var entity = await _unitofwork.PersonRepository.Get(id);

            if(entity == null)
            {
                _logger.LogError($"Person doesnot found  : {id}");
                throw new  Exception("Person not found");
            }

            entity.IsDeleted = true;
            await _unitofwork.PersonRepository.SetIsDeleteTrue(entity);
            await _unitofwork.Save();

            return true;
        }
    }
}
