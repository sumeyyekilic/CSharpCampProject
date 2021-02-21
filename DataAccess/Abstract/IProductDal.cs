using Core.DataAccess;
using DataAccess.Concrete.DTOs;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal :IEntityRepository<Product>  // buaraya :IEntityRepository<Product> eklenerek aşağıdaki kodları yazmadan hazırlamam ve erişemem sağlandı. 
    {
        //****AŞAĞIDAKİ KODLAR IEntityRepository.cs e taşınarak 
        //"Generic Repository Desing Pattern" OLUŞT. ADINA 
        //STANDART YAPIYA GEÇİLDİ!

        //List<Product> GetAll(); //başk bir katmanı kul. istiyorsan referans verirs,n (product gibi) 
        //                        //sadece entites katmanı ref. verildi. bunu yapınca usingi ekleme seçeneği geldi.

        ////interface metotları defoult publictir. Add, Update,Delete de old. gibi
        //void Add(Product product);
        //void Update(Product product);
        //void Delete(Product product);

        ////ürünleri kategoriye göre listele :
        //List<Product> GetAllByCatgory(int categoryId);

        List<ProductDetailDto> GetProductDetail();

    }
}
