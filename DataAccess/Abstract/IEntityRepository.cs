﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //generic repository desing pattern yapmak için eklendi
    public interface IEntityRepository<T>
    {
        //IProductDal ve ICategoryDal içeirisindeki operasyonlar kesildi.
        //Product veya Category yerine ise o an  ne yazacaksam o gelmeli.
        //BUna T dersek :  IEntityRepository<T>   BU ŞEKİLDE YAPI OLUŞTURULUR!

        //aşağıdaki yapı diğer classlarda tekrarlanıyor. bu yuzden T ifadesi eklendi.
        List<T> GetAll(); 
          
        void Add(T entity);  
        void Update(T entity);
        void Delete(T entity);

       
        List<T> GetAllByCatgory(int categoryId);

    }
}
