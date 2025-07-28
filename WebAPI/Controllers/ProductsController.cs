using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string Hello([FromQuery]  string name, [FromQuery] string surname)   // url de tanımlanırken ad=ahmet&soyad=dev şeklinde yazılır.
        {
            return "Hello" + name + "" + surname;
        }

        [HttpGet("tobeto")]
        public string Tobeto()
        {
            return "Tobeto";
        }

        [HttpGet("{username}")]
        public string Course([FromRoute]string username)
        {
            return "Course";
        }

        [HttpPost]
        public string GoodBye([FromBody] Product product)
        {
            return "GoodBye";
        }
    }
}

// Route Parameters, Query String => Get isteklerinde popüler.
// Body => POST, PUT.
// Headers => Yan bilgileri içerir (İstek nereden atıldı? İstek atılırken kullanılan dil neydi? Hangi dil tercih ediliyor? şeklindedir.
 