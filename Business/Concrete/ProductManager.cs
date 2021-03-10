using Business.Abstract;
using Business.BusinessAspects.Autofact;
using Business.Constants;
using Business.CSS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.DTOs;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
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

        ICategoryService _categoryService;   // bir **Entity manager** kendisi hariç başka dalı enjekte edemez. Bunun yerine başka bir servisi enjekte edebilriiz.
        //ILogger _Ilogger;
        public ProductManager(IProductDal productDal, /*ILogger logger,*/ ICategoryService categoryService) //ctora ben product olarak ıloggera iht duyuyorum dedim.
        {
            _categoryService = categoryService;
            //_Ilogger = logger;
            _productDal = productDal;
        }

        //Claim
        [SecuredOperation("product.add, addmin")]
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

            //ValidationTool.Validate(new ProductValidator(), product);
            //loglama
            //cace
            //performans yöentimi
            //transaction
            //yetkilendirme yap  vs... yine bir sürü kod olacak burada
            //bunun yerine 
            //[Validate] yapısını kurup metodun üzerine yazarsam gidip o parametreyi okuycak ilgili validaterı bulup validation yapacak

            //_Ilogger.Log(); //bunu rty içine de ekleyebilirim
            //try
            //{
            //    _productDal.Add(product);

            //    //return new Result(true,"");  //bu satırı eklemezsek Add kızar!

            //    //Biz istekte bulunan kişiye yaptığı işlem sonucunda işlemin başarısız olduğu mesajı veya yaptığı işlemin başlarılı old yapıları burada oluşturacağız.

            //    return new SuccessResult(Messages.ProductAdded);
            //}
            //catch (Exception exception)
            //{
            //    _Ilogger.Log(); //
            //}

            //bir kategoride en fazla 10 ürün olabilir : BU YÖNTEM YANLIŞ :) çünkü iş kuralı ve başka iş kuralları da olabilir.update yaparken de bu kural geçerli çünkü.. 
            //iş kuralı kategoride 15 ürün olabilir diye değiştiğinde gelip burrda değişitirmem kötü bir kodlama. yazılımcı kendini tekrar edecek. ve updatee metodunda bunu güncellemeyi unuttuysa hata yapar.
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count; //o kategorideki ürünleri bulursun
            //if(result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCounOfCategoryError); 
            //}

            ////yukardaki  ürün iş kuralı yerine yazdığım standart metodu aşağıdaki gibi kullandım :
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Sussess)
            //{
            //    if (CheckIfProductNameExist(product.ProductName).Sussess)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded); //ErrorDataResult deyip birşey de döndürebiliriz
            //    }
            //}

            //Polymorphism   : YUKARDAKİ çirkin koda gerek kalmadı. core katmnına yazdığım standat polymorphism ile şu şekilde 1000 tane bile iş kuralı gönderebilrim.
            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExist()); // iş kurallarını çalıştıracak. isterse bin tane iş kuralı olsun

            if (result != null)//kurala uymayan bir durum oluşmuşsa 
            {
                return result;

            }

            return new ErrorResult();
        }


        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları

            if (DateTime.Now.Hour == 22)
            {//diyelim ki 22 de ürünlerin listelenmesini kapatmak istiyoruz,
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

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
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            //burada ADD metodundan daha farklı durumla olabilir. sadece mantık yakalamak için yukardaki kodu aldım
            //Aşağıdaki kod sektorde yaygın olarak yapılan bir hatadır!
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count; //o kategorideki ürünleri bulursun
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCounOfCategoryError);
            //}



            return new ErrorResult();
        }
        //neden private? bu metodun sadece bu classın içeriisnde kullanılmasını istiyorumdur.bu iş kuralı parcağı olduğu için. 
        //eğer farklı managerlarda kulllanayım öle bir senaryom varsa bunu pub lic yapayım hatasına düşme. iş kuralı parçacığı çünkü.
        //bu kuralı bir kere yazdım ve metodu artık istediğim yerde kullanabilrim.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) // kategoride ki ürün sayısının kurallara uygunluğunu doğrula 
        {
            //Select count(*) from products where categoryId=1   --> arka planda bu çalışır
            //bir kategoride en fazla 10 ürün olabilir 
            //yukarda yazığım çirkin kodu alıp
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count; //o kategorideki ürünleri bulursun  //burası arka planda bizim için bir linq query oluşturuyor , veritabanına o query i gönderiyor.
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCounOfCategoryError);
            }

            return new SuccessResult(); //success resultı boş geçiyorum 
                                        // çünkü bu kuraldan geçiyoruz. kullanıcıya giidp bu kuraldan geçtin demeye gerek yok :)
        }
        private IResult CheckIfProductNameExist(string productName) // kategoride ki ürün sayısının kurallara uygunluğunu doğrula 
        {
            //aynı isimde ürün eklenemez: 
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();  // any() uyan kayıt var mı ?
            if (result)//eğer böyle bir data varsa
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists); //ProductNameAlreadyExists : böyle bir ürün zaten var demek
            }
            return new SuccessResult();
        }

        //eğer mevcut kategri sayısı 15i geçtiyse sisteme yeni bir ürün eklenemez. (miksrodervis mimarilere nasıl bakmamız gerekiyor ?'u iyi anlarız.. sektorde çok az kişinin yaptığı :) )
        private IResult CheckIfCategoryLimitExist()
        { 
            var result = _categoryService.GetAll();  
            if(result.Data.Count>15)
            {
                return new ErrorResult(Messages.CatgoryLimitedExists); //BU kuralı neden  category service de yazmadık ?  eğer kat yazıyorsak bu tek başına servis olurdu ama bu bizim productın category servisini nasıl yorumdaığı olayıdır. o yuzden product içine yazarız. ve kontrol altına almış oluruz
                                                                        //eğer bu kuralı cat. managera ayazarsak bu tek başına servis olur.bu metod o servisi kullanan bir ürünün onu nasıl ele aldığıyla ilgilidr.  
            }
            return new SuccessResult();
        }
    }
}
