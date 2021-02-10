using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    //iş katmanında kullanacağımız servis operasyonları
    public interface IProductService
    {
        //DATAACCESS VE ENTİTİES ref olarak verilidi : Product uing eklenebilmesi için
        List<Product> GetAll(); //ürün listesi döndürüyo.
        List<Product> GetAllByCategoryId(int id);
        List<Product> GetByUnitPrice(decimal min, decimal max);
        //List<ProductDetailDto> GetProductDetails();
        //Product GetById();
        IResult Add(Product product); //void yerine IResult dedim.
    }
}
