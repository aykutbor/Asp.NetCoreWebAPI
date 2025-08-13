using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                string errorMessage;
                if (exception is BusinessException)
                {
                    errorMessage = exception.Message;
                }
                else
                {
                    errorMessage = "Bilinmeyen Hata";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                await context.Response.WriteAsync(errorMessage);

            }
        }
    }
}

// Middleware'ler uygulamayı dikine böler ve uygulamanın nasıl cevap vermesi gerektiğini yönlendirebilir. 
// RequestDelegate ise, bu bölme işleminden sonrasının çalışması için kullanılan class'dır. 

// Uygulama çalışırken önce try bloğuna gelir, buradan next ile geçerse isteğe gider. Program.cs içerisinde middleware tanımlandığı için uygulamanın genelinde çalışır.
