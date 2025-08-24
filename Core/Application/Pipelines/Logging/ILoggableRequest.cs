using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Logging
{
    public interface ILoggableRequest{} // İçi boş olur sadece imzalamak için kullanılır.
}
// Bir behavior'dan hangi request'lerin etkilenip etkilenmediğini belirlemek için kullanılır.

// LoggingBehavior içerisinde kıstas olarak ILoggableRequest olarak eklenir. Böylece LoggingBehavior, tüm sisteme eklenmiş olsa dahi kendi içerisinde sadece
// şu kıstası "where TRequest : IRequest<TResponse>, ILoggableRequest"
// uygulayacağı için, 
// ILoggable interface'ini almamış yapılar için devreye girmeyecek.
