using Business.Abstracts;
using Business.Concretes;
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
            services.AddScoped<IProductService, ProductManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}

// Program.cs içerisinde business katmanı ile ilgili konfigürasyonları tek tek eklemek yerine hepsini bu extension class içerisinde tanımlayacağız. Program.cs içerisinde sadece "AddBusinessServices" metodu tanımlanacak. Böylece program.cs içerisinde daha temiz bir kullanım sağlamış olacağız.
