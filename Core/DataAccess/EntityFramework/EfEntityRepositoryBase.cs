using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using System.Linq.Expressions;
using System.Linq;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new() //Dbcontext entity fw'den gelir.
    {
        public void Add(TEntity entity) //PRODUCT yerine TEntity yazdım
        {
            //bir classı new'lediğimizde garbage collector belli bir zamanda düzenli olarak gelir
            //ve onu bellekten atar.

            //using içerisine yazılan nesneler using bitince, garbage collector'e gelip, beni bellekten at diyor.
            //çünkü context nesnesi biraz pahalı.
            //bu c#'ın disposable pattern implamentasyonudur. (belleği hızlıca temizle)
            using (TContext context = new TContext()) //bu hareket daha performanslı ürün geliştirmeyi sağlar
            {
                // NorthwindContext yerine TContext olaak değiştirdim..
                var addedEntity = context.Entry(entity); //bu bir ekleme, eşleşme yapmaz. Referansı Yakala!
                addedEntity.State = EntityState.Added;  //veri kaynağı ile ilişkilendirdim. O bir nesne!
                context.SaveChanges(); //ekle! save changes o işlemi yapar..

            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext()) //bu hareket daha performanslı ürün geliştirmeyi sağlar
            {
                var deletedEntity = context.Entry(entity); //Referansı Yakala!
                deletedEntity.State = EntityState.Deleted;  //O bir nesne!
                context.SaveChanges(); //sil! save changes o işlemi yapar..

            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //eğer filtre göndermemişse veritabanındaki ilgili tüm tabloyu getir
                return filter == null ? context.Set<TEntity>().ToList() :   //select *from product'ı döndürür.
                    context.Set<TEntity>().Where(filter).ToList();     //filtre vermişse o filtreyi uygula , ona göre datayı listele

            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }

            //standart gördüğümüz yerde ona generic base getirmeliyiz. burada henuz uygulamadım
        }

        public void Update(TEntity entity)
        {
            //bu kodlar aslında tekrar ediyor. soyutlama teknikleri kulllanılmalı !

            using (TContext context = new TContext()) //bu hareket daha performanslı ürün geliştirmeyi sağlar
            {
                var updateEntity = context.Entry(entity); //Referansı Yakala!
                updateEntity.State = EntityState.Modified;  //O bir nesne!
                context.SaveChanges(); //güncelle! save changes o işlemi yapar..

            }
        }
    }
}
