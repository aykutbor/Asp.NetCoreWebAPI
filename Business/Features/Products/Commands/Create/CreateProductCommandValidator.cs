using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(i => i.Stock).GreaterThanOrEqualTo(1);
            RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(1);
            RuleFor(i => i.CategoryId).GreaterThanOrEqualTo(1);
            
            
            // Kendi Kuralımız
            //RuleFor(i => i.Name).Must(name => name.StartsWith("A")); // Knedi oluşturduğumuz örnek kural.

            RuleFor(i => i.Name).Must(StartsWithA).WithMessage("İsim alanı A harfi ile başlamalıdır.");
        }

        public bool StartsWithA(string name)
        {
            return name.StartsWith("A");
        }
    }
}

// Validasyon kullanılırken, AbstractValidator<Valide edilecek class> şeklinde kullanılır. AbstractValidator, FluentValidation'dan gelir.

// Yukarıdaki örnek kuralı Must ile tanımlarken fonsiyonu parantez içerisinde yazmak yerine ayrı oluşturup, Must'a fonksiyonun referansını verebiliriz.