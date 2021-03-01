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

        [HttpGet]
        public /*List<Product>*/ string Get()  //uydurma bir metot, string değer döndürüyor. (metin)
        {
            //kötü kod yazalım:
            //Dependency chain  : bağımlılık servisi oluştu.
            //IProductService productService = new ProductManager(new EfProductDal()); //bağımlı old şey product manager'dıır.
            

            
            var resultt = _productService.GetAll();

            //apiyi geliştiren ekip farklıysa buraya bakıp içerisinde datat bulunan yapı veriyor diyebilir.
            return resultt.Message; //sistem bakımda hatası basar. //product manager daki GetALL da sistem saati için uyduruk bi kod yazmıştım
                                    //burayı kullanacak kişiye doğru bilgi vermemişz gerekiyor her zaman.
                                    //result yapısı bu şekilde sonuçlar ççıkartır.

        }

    }
}
