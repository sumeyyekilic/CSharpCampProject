using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir
    //generic repository desing pattern yapmak için eklendi
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        //IProductDal ve ICategoryDal içeirisindeki operasyonlar kesildi.
        //Product veya Category yerine ise o an  ne yazacaksam o gelmeli.
        //BUna T dersek :  IEntityRepository<T>   BU ŞEKİLDE YAPI OLUŞTURULUR!

        //aşağıdaki yapı diğer classlarda tekrarlanıyor. bu yuzden T ifadesi eklendi.
        List<T> GetAll(Expression<Func<T,bool>> filter=null); //eticaret uyg.daki filtreleme aslında.... 
                                                             //(p=>p.CategoryId==2);
        void Add(T entity);  
        void Update(T entity);
        void Delete(T entity);

       
        //List<T> GetAllByCatgory(int categoryId);

    }
}
