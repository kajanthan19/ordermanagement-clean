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

    public class GetAllPersonQuery : IRequest<IList<PersonDto>>
    {
    }

    public class GetAllPersonQueryHandler : IRequestHandler<GetAllPersonQuery, IList<PersonDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPersonQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<PersonDto>> Handle(GetAllPersonQuery request, CancellationToken cancellationToken)
        {

            var entities = await _unitOfWork.PersonRepository.GetAll(x=>x.IsDeleted == false);
            var result = _mapper.Map<IList<PersonDto>>(entities);

            return result;

        }
    }


}
