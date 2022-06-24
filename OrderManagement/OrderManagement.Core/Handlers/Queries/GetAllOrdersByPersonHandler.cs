using AutoMapper;
using MediatR;
using OrderManagement.Domain.Dtos;
using OrderManagement.Domain.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Core.Handlers.Queries
{

    public class GetAllOrdersByPersonCommand : IRequest<IList<OrderDto>>
    {
        public string CreatedBy { get; }

        public GetAllOrdersByPersonCommand(string createdBy)
        {
            CreatedBy = createdBy;
        }
    }

    public class GetAllOrdersByPersonHandler : IRequestHandler<GetAllOrdersByPersonCommand, IList<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersByPersonHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<OrderDto>> Handle(GetAllOrdersByPersonCommand request, CancellationToken cancellationToken)
        {
            var createdby = request.CreatedBy;
            var entities = await _unitOfWork.OrderRepository.GetAll(x=>x.CreatedBy == createdby && x.IsDeleted == false);
            var result = _mapper.Map<IList<OrderDto>>(entities);

            return result;

        }
    }
}
