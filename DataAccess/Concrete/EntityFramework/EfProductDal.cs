﻿using Core.DataAccess.EntityFramework;
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
    public class EfProductDal : EfEntityRepositoryBase <Product, NorthwindContext>, IProductDal
    {
        //IProductDal içerisindeki tüm operasyonlar EfEntityRepositoryBase içerisinde olduğu için
        //EfEntityRepositoryBase'i çözümlemem yeterli.
        
        
        // EfProductDal : EfEntityRepositoryBase <Product, NorthwindContext>, IProductDal
        // yukardaki satırı yaparak : EfProductDal 'da tüm VT operasyonları hazır hale gelir.


    }
}
