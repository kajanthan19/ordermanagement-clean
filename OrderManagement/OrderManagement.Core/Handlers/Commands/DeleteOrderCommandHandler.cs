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

    public class DeleteOrderCommand : IRequest<bool>
    {

        public int Id { get; }
        public DeleteOrderCommand(int id)
        {
            this.Id = id;
        }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger, IUnitOfWork unitofwork,
         IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var entity = await _unitofwork.OrderRepository.Get(id);

            if (entity == null)
            {
                _logger.LogError($"Order doesnot found  : {id}");
                throw new Exception("Order not found");
            }

            entity.IsDeleted = true;
            await _unitofwork.OrderRepository.SetIsDeleteTrue(entity);
            await _unitofwork.Save();

            return true;
        }
    }
}
