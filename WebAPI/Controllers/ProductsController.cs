using Business.Abstracts;
using Business.Features.Products.Commands.Create;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

       

        [HttpPost("add")]
        public async Task Add([FromBody] CreateProductCommand command)
        {
            await _mediator.Send(command);
        }

    }

}

// _mediator.Send(); -> İçerisine mediator'ün beklediği tipte bir request alan, ve bu request'i  mediator'ün doğru noktaya yönlendirerek işlediği bir yapı demektir.

// Senkron ve Asenkron

// Senkron => Bir olayın bir metodun çalışma anında bir satırı çalıştırırken o satırı bitirmeden alt satıra geçmemesine senkron işlem denir.

// Asenkron => Bir satırı başlatıp bitmesini beklemeden alt satıra geçebilen ve işlemi bloklamayan yapılara asenkron yapılar denir.

// DipNot: bir asenkron metodun başına await geldiği zaman senkron program gibi çalışır, yani işlemin bitmesini bekler. Yukarıdaki örnekte, sadece Task.Delay(5000) dediğimiz zaman 5 saniye beklemez bir sonraki satıra hemen geçer, ama await Task.Delay(5000) dediğimiz zaman, 5 saniye bekler ondan sonra alt satıra geçer.

// Teknik: Bir asenkron işlem, çalıştığı bilgisayardaki işlemcinin thread'lerinin hepsini bloklamaz await kullanılsa bile. Senkron işlem ise tüm thread'leri blokladığı için önce kendi işleminin bitmesini bekler ondan sonra diğer işlemi çalıştırır.

// Veritabanı işlemleri gibi, dosya okuma yazma gibi, bir trafik içerisinde en çok zaman harcayacağımız yapılarda kullanıyoruz.