using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //[HttpGet]
        //public string Get()  //uydurma bir metot, string değer döndürüyor. (metin)
        //{
        //    return "selamm";
        //}

        //Loosely coupled : gevşek bağımlılık
        IProductService _productService; //naming convention

        public ProductsController(IProductService productService) //controller'a gevşek bağllık : ıproductservice bağlı
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult /*List<Product>*/ /*string*/ GetAll()  //uydurma bir metot, string değer döndürüyor. (metin)
        {
            //kötü kod yazalım:
            //Dependency chain  : bağımlılık servisi oluştu.
            //IProductService productService = new ProductManager(new EfProductDal()); //bağımlı old şey product manager'dıır.
            

            
            var resultt = _productService.GetAll();

            //apiyi geliştiren ekip farklıysa buraya bakıp içerisinde datat bulunan yapı veriyor diyebilir.
            //return resultt.DataMessage; //sistem bakımda hatası basar. //product manager daki GetALL da sistem saati için uyduruk bi kod yazmıştım
            //burayı kullanacak kişiye doğru bilgi vermemişz gerekiyor her zaman.
            //result yapısı bu şekilde sonuçlar ççıkartır.

            if (resultt.Sussess)
            {
                return Ok(resultt.Data); //oK =200 döndür ve içinde de reultt datası  (object = tüm veri tiiplerinin atasıdır, herşeyi atayabilirsin demektir)
            }
            return BadRequest(resultt.Message);  //eğer başarısızsa
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Sussess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add( Product product) //controllerın bildiği yer burası, o yuzden istediğin nesneyi metoda parametre olarak eklerim
        {
            var result = _productService.Add(product);
            if (result.Sussess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        

    }
}
