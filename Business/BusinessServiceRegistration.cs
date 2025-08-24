using Business.Abstracts;
using Business.Concretes;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddMediatR(config => { 
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());  // Uygulamanın tamamındaki request ve request handler'ları bulmak için kullanılır.
                // Sıralama önemlidir.
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // <,> kıstas varsa o kıstastak tüm tipleri kabul et anlamına gelir.
            });
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Fluent Validation -> Dependency Injection kullanımı için. Assembly'lere bakar ve ne kadar validatör varsa, hepsini servislerin içerisine ekle. Bu sayede tüm validator'lar sistemde dependency injection içerisinde kullanılabilir halde.

            return services;
        }
    }
}

// Program.cs içerisinde business katmanı ile ilgili konfigürasyonları tek tek eklemek yerine hepsini bu extension class içerisinde tanımlayacağız. Program.cs içerisinde sadece "AddBusinessServices" metodu tanımlanacak. Böylece program.cs içerisinde daha temiz bir kullanım sağlamış olacağız.

// AddOpenBehavior, verilen pipeline sıralamasıyla, verilen behavior'ları kullan. Önce eklenen önce çalışır.
// typeof, eklemek istenilen behavior'un türünü belirtmek için kullanılır.
