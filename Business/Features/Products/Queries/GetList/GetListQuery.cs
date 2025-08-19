using Business.Features.Products.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Products.Queries.GetList
{                               // Mediator Request
    public class GetListQuery : IRequest<ListProductResponse>
    {
        // Request'in istediği alanlar
        public int Page { get; set; }
        public int PageSize { get; set; }


        // Request'i Handle edecek class                   // Komut - Dönüş tipi
        public class GetListQueryHandler : IRequestHandler<GetListQuery, ListProductResponse>
        {
            // Dependency, Handle - Service gibi çalışır
            public Task<ListProductResponse> Handle(GetListQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
