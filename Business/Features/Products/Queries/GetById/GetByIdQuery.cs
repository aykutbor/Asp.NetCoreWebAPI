using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstracts;
using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Products.Queries.GetById
{
    public  class GetByIdQuery : IRequest<GetByIdProductResponse>
    {
        public int Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetByIdProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<GetByIdProductResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            Product? products = await _productRepository.GetAsync(p => p.Id == request.Id);

            if (products is null)
                throw new BusinessException("Böyle bir veri bulunamadı.");

            GetByIdProductResponse response = _mapper.Map<GetByIdProductResponse>(products);

            return response;
        }
    }
}
