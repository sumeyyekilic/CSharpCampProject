using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal
    {
        //****AŞAĞIDAKİ KODLAR IEntityRepository.cs e taşınarak 
        //"Generic Repository Desing Pattern" OLUŞT. ADINA 
        //STANDART YAPIYA GEÇİLDİ!

        //List<Category> GetAll(); //başk bir katmanı kul. istiyorsan referans verirs,n (product gibi) 
        //                        //sadece entites katmanı ref. verildi. bunu yapınca usingi ekleme seçeneği geldi.

        ////interface metotları defoult publictir. Add, Update,Delete de old. gibi
        //void Add(Category category);
        //void Update(Category category);
        //void Delete(Category category);

        ////ürünleri kategoriye göre listele :
        //List<Category> GetAllByCatgory(int categoryId);
    }
}
