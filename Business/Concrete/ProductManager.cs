using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //veri iş kodlarını geçiyorsa veri erişim çağırlır.

        //InMermoryProductDal ınMermoryProductDal = new InMermoryProductDal();  
        //Yukardaki şekilde yaparsan, senin iş kodlarının tamamı bellek ile çalışıor. gerçek VT 'A geçeceğin zaman; örneğin bankacılıkta binlerce operasyon uygulaması var! bunların hepsini değiştimen gerekir... 

        //bir iş sınıfı başka sınıfları new'lemez 
        //Bunun yerine ; InMemoryProductDal _ınMemoryProductDal;

        IProductDal _productDal; //soyut nesne ile bağlant kuracağım.
                                 //ne entity framework nede entity ismi geçecek.

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            //iş kodları
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            throw new NotImplementedException();
        }
    }
}
