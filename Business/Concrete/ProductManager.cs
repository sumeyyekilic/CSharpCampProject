using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult Add(Product product)
        {
            //business kodlar buraya yazılır.
            //ürünü eklemeden önce kodları buraya yazarız, eğer geçerlşiyse ürün eklernir.

            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }

            _productDal.Add(product);

            //return new Result(true,"");  //bu satırı eklemezsek Add kızar!

            //Biz istekte bulunan kişiye yaptığı işlem sonucunda işlemin başarısız olduğu mesajı veya yaptığı işlemin başlarılı old yapıları burada oluşturacağız.

            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları

            if (DateTime.Now.Hour == 22)
            {//diyelim ki 22 de ürünlerin listelenmesini kapatmak istiyoruz,
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public SuccessDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        //public Product GetById()
        //{
        //    return _productDal.Get();
        //}

        public SuccessDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice > min && p.UnitPrice < max));

        }

        IDataResult<List<Product>> IProductService.GetAllByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        List<Product> IProductService.GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);  //iki fiyat aralığında olan datayı getirir.
        }
    }
}
