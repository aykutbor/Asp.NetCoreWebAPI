using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleWare
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/json"; // http isteğinde hata olursa json formatında döner.
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                
                if (exception is BusinessException)
                {
                    ProblemDetails problemDetails = new ProblemDetails();
                    problemDetails.Title = "Business Rule Violation";
                    problemDetails.Detail = exception.Message;
                    problemDetails.Type = "BusinessException";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails)); // Class'ı Json formatında string olarak verir.
                }
                else if(exception is ValidationException)
                {
                    // Casting
                    ValidationException validationException = (ValidationException) exception;
                    ValidationProblemDetails validationProblemDetails = new ValidationProblemDetails(validationException.Errors.ToList());
                    await context.Response.WriteAsync(JsonSerializer.Serialize(validationProblemDetails));
                }
                else
                {

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

               

            }
        }
    }
}

// Middleware'ler uygulamayı dikine böler ve uygulamanın nasıl cevap vermesi gerektiğini yönlendirebilir. 
// RequestDelegate ise, bu bölme işleminden sonrasının çalışması için kullanılan class'dır. 

// Uygulama çalışırken önce try bloğuna gelir, buradan next ile geçerse isteğe gider. Program.cs içerisinde middleware tanımlandığı için uygulamanın genelinde çalışır.
