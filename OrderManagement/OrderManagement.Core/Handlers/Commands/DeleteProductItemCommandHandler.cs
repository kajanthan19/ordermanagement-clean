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
    public class DeleteProductItemCommand : IRequest<bool>
    {
        public int Id { get; }
        public DeleteProductItemCommand(int id)
        {
            this.Id = id;
        }
    }

    public class DeleteProductItemCommandHandler : IRequestHandler<DeleteProductItemCommand, bool>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteProductItemCommandHandler> _logger;

        public DeleteProductItemCommandHandler(ILogger<DeleteProductItemCommandHandler> logger, IUnitOfWork unitofwork,
         IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;
            var entity = await _unitofwork.ProductItemRepository.Get(id);

            if (entity == null)
            {
                _logger.LogError($"Product Item doesnot found  : {id}");
                throw new Exception("Product Item not found");
            }

            entity.IsDeleted = true;
            await _unitofwork.ProductItemRepository.SetIsDeleteTrue(entity);
            await _unitofwork.Save();

            return true;
        }
    }

}
