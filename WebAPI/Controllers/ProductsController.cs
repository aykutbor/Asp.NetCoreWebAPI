using Business.Abstracts;
using Business.Dtos.Product;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<ProductForListingDto>> GetAll()
        {
            return await _productService.GetAll();
        }

        [HttpPost("add")]
        public async Task Add([FromBody] ProductForAddDto product)
        {
            await _productService.Add(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            return Ok(product);
        }

        [HttpPut]
        public void Update(Product product)
        {
            _productService.Update(product);
        }

        [HttpGet("Senkron")]
        public string Sync()
        {
            Thread.Sleep(5000); // 5 saniye beklet, ardından bir sonraki satıra geç.
            return "Sync endpoint";
        }

        [HttpGet("Asenkron")]
        public async Task<string> Async()
        {
            await Task.Delay(5000); // await ise beklemek istediğimiz işlemler için kullanılır, örnek db'den veri çekilmesi gibi işlemler.
            return "Async endpoint";
        }

    }

}

// Senkron ve Asenkron

// Senkron => Bir olayın bir metodun çalışma anında bir satırı çalıştırırken o satırı bitirmeden alt satıra geçmemesine senkron işlem denir.

// Asenkron => Bir satırı başlatıp bitmesini beklemeden alt satıra geçebilen ve işlemi bloklamayan yapılara asenkron yapılar denir.

// DipNot: bir asenkron metodun başına await geldiği zaman senkron program gibi çalışır, yani işlemin bitmesini bekler. Yukarıdaki örnekte, sadece Task.Delay(5000) dediğimiz zaman 5 saniye beklemez bir sonraki satıra hemen geçer, ama await Task.Delay(5000) dediğimiz zaman, 5 saniye bekler ondan sonra alt satıra geçer.

// Teknik: Bir asenkron işlem, çalıştığı bilgisayardaki işlemcinin thread'lerinin hepsini bloklamaz await kullanılsa bile. Senkron işlem ise tüm thread'leri blokladığı için önce kendi işleminin bitmesini bekler ondan sonra diğer işlemi çalıştırır.

// Veritabanı işlemleri gibi, dosya okuma yazma gibi, bir trafik içerisinde en çok zaman harcayacağımız yapılarda kullanıyoruz.