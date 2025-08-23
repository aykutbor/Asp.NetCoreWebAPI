using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("ValidationBehavior Devreye Girdi");

            TResponse response = await next();
            return response;
        }
    }
}

// TRequest - TResponse, her command ve query'e ve respons'a uygulanabilmesi için generic yapılır.

// IPipelineBehavior, request ve response olarak mediator'un request tipinde bir beklenti içerisinde olduğu için "where" kıstası olmazsa hata verir. 

// "ValidationBehavior<TRequest" -> request her zaman mediator'dan türeyen bir request olmalıdır.

// next, Delegate olduğu için direkt olarak fonksiyon gibi kullanılabilir.
