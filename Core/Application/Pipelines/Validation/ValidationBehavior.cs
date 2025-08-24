using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace Core.Application.Pipelines.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators; // Bir request'in birden fazla validator'ü olabileceği için IEnumarable tipinde yazılır.

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("Validation çalıştı..");
            ValidationContext<object> context = new ValidationContext<object>(request);

            var errors = _validators
                .Select(validator => validator.Validate(context))   // Validatör'lere git ve Validate metodunu çalıştır.
                .SelectMany(result => result.Errors)                    // Validator'lerin hata mesajlarını listele. SelectMnay -> Birden çok validator hatası için.
                // .GroupBy(keySelector: p=>p.PropertyName, resultSelector: (propertyName, errors) => new ValidationException())
                .ToList();

            if(errors.Any())
                throw new ValidationException(errors.Select(e => e.ErrorMessage).ToList());


            TResponse response = await next();
            return response;
        }
    }
}

// TRequest - TResponse, her command ve query'e ve respons'a uygulanabilmesi için generic yapılır.

// IPipelineBehavior, request ve response olarak mediator'un request tipinde bir beklenti içerisinde olduğu için "where" kıstası olmazsa hata verir. 

// "ValidationBehavior<TRequest" -> request her zaman mediator'dan türeyen bir request olmalıdır.

// next, Delegate olduğu için direkt olarak fonksiyon gibi kullanılabilir.

// ValidationContext, entity framework'deki dbContext gibi validasyonu yöneten genel bir bağlam.

// AddValidatorsFromAssembly, sayesinde tüm valitor'lar dependency injection container'a eklenir. Buradaki constructor yapısı ile container'da validator'ların çözülmesi beklenir.

