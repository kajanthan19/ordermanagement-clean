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
    public class GetAllProductItemsQuery : IRequest<IList<ProductItemDto>>
    {
    }
    public class GetAllProductItemsQueryHandler : IRequestHandler<GetAllProductItemsQuery, IList<ProductItemDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllProductItemsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<ProductItemDto>> Handle(GetAllProductItemsQuery request, CancellationToken cancellationToken)
        {
            
                var entities =  await _unitOfWork.ProductItemRepository.GetAll(x=>x.IsDeleted == false);
                var result = _mapper.Map<IList<ProductItemDto>>(entities);

                return result;
      
        }
    }
}
