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

        [HttpGet]
        public List<Product> Get()  //uydurma bir metot, string değer döndürüyor. (metin)
        {
            //kötü kod yazalım:
            IProductService productService = new ProductManager(new EfProductDal);
            var resultt = productService.GetAll();

            //apiyi geliştiren ekip farklıysa buraya bakıp içerisinde datat bulunan yapı veriyor diyebilir.
            return resultt.Data;
        }

    }
}
