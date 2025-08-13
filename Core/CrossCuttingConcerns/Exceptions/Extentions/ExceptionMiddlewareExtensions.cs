using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionMiddlewareExtensions(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleWare>(); // Middleware olarak kullanmak istediğimiz class belirtilir.
        }
    }
}

// Extensions yazılacak class'ın ve metodun "static" olması gerekiyor.

// Teknik: Bir metodu genişletmek, program.cs içerisinde app. ile başlayan alanı içerisine bir fonksiyon eklemek için kaynak koduna ihtiyaç duyulur. Kaynak koduna ihtiyaç duymadan "bunun içine bu özelliği kazandır" demek. 

// "ConfigureExceptionMiddlewareExtensions" isimli metot program.cs içerisine dahil olur. Bu sayede core katmanındaki bu yapı çalıştırılır. Kısacası, program.cs içerisnde tek tek bağımlılık eklemek yerine bir metotu genişletiriz ve onu tanımlarız. Genişlettiğimiz metotdun içerinde tanımlanacak yapıları ekleriz örneğin ExceptionMiddleware yapısı.

// Extend edilecek alanı parametre olarak alıp başına this yazılır (this IApplicationBuilder app) This keyword'ü ile bu parametrenin yapının kendisi olduğunu belirtmiş oluruz ve bu yapıyı genişlettiğimiz anlamına gelir.

// Özet, core paketlerini tek tek program.cs içerisinde tanımlamak yerine burada tanımlayıp, bu metodu da program.cs içerisinde bir defa tanımlayacağız.