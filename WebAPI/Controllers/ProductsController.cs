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
            return new List<Product>
            {
                new Product{ProductId =1 , ProductName="Mobile"},
                new Product{ProductId=2 ,ProductName="Bike"}
            };
        }

    }
}
