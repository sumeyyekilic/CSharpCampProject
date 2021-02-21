using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //bir classı new'lediğimizde garbage collector belli bir zamanda düzenli olarak gelir
            //ve onu bellekten atar.

            //using içerisine yazılan nesneler using bitince, garbage collector'e gelip, beni bellekten at diyor.
            //çünkü context nesnesi biraz pahalı.
            //bu c#'ın disposable pattern implamentasyonudur. (belleği hızlıca temizle)
            using (NorthwindContext context= new NorthwindContext()) //bu hareket daha performanslı ürün geliştirmeyi sağlar
            {
                var addedEntity = context.Entry(entity); //bu bir ekleme, eşleşme yapmaz. Referansı Yakala!
                addedEntity.State = EntityState.Added;  //veri kaynağı ile ilişkilendirdim. O bir nesne!
                context.SaveChanges(); //ekle! save changes o işlemi yapar..

            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext()) //bu hareket daha performanslı ürün geliştirmeyi sağlar
            {
                var deletedEntity = context.Entry(entity); //Referansı Yakala!
                deletedEntity.State = EntityState.Deleted;  //O bir nesne!
                context.SaveChanges(); //sil! save changes o işlemi yapar..

            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context= new NorthwindContext() )
            {
                //eğer filtre göndermemişse veritabanındaki ilgili tüm tabloyu getir
                return filter == null ? context.Set<Product>().ToList() :   //select *from product'ı döndürür.
                    context.Set<Product>().Where(filter).ToList();     //filtre vermişse o filtreyi uygula , ona göre datayı listele

            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
               return context.Set<Product>().SingleOrDefault(filter);
            }

            //standart gördüğümüz yerde ona generic base getirmeliyiz. burada henuz uygulamadım
        }

        public void Update(Product entity)
        {
            //bu kodlar aslında tekrar ediyor. soyutlama teknikleri kulllanılmalı !

            using (NorthwindContext context = new NorthwindContext()) //bu hareket daha performanslı ürün geliştirmeyi sağlar
            {
                var updateEntity = context.Entry(entity); //Referansı Yakala!
                updateEntity.State = EntityState.Modified;  //O bir nesne!
                context.SaveChanges(); //güncelle! save changes o işlemi yapar..

            }
        }

        public List<Product> GetAllByCatgory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
