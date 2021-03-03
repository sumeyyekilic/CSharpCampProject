using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.DTOs;
using Entities.Concrete;
using FluentValidation;
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

        //validasyon yok ama  ; Aspect ekledim 
        [ValidationAspect(typeof(ProductValidator))] //add metodunu ProductValidator göre kodla
        public IResult Add(Product product)
        {
            //business kodlar buraya yazılır.
            //ürünü eklemeden önce kodları buraya yazarız, eğer geçerlşiyse ürün eklernir.

            //Validation  --!! burdaki kodlaı fluent validasyonda yazdım !
            //if (product.UnitPrice <= 0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}


            //if (product.ProductName.Length < 2) //ürünün ismi min 2 karaker olmalı
            //{
            //    //magic string :  return new ErrorResult("ürünün ismi min 2 karaker olmalıdır!");
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            ValidationTool.Validate(new ProductValidator(), product);
            //loglama
            //cace
            //performans yöentimi
            //transaction
            //yetkilendirme yap  vs... yine bir sürü kod olacak burada
            //bunun yerine 
            //[Validate] yapısını kurup metodun üzerine yazarsam gidip o parametreyi okuycak ilgili validaterı bulup validation yapacak


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

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        }


        //public SuccessDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        //{
        //    return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice > min && p.UnitPrice < max));

        //}

        //public SuccessDataResult<List<ProductDetailDto>> GetProductDetails()
        //{
        //    return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetail());
        //}

        IDataResult<List<Product>> IProductService.GetAllByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));  //iki fiyat aralığında olan datayı getirir.
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetail());

        }
    }
}
