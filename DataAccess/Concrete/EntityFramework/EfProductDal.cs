﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.DTOs;
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
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        //IProductDal içerisindeki tüm operasyonlar EfEntityRepositoryBase içerisinde olduğu için
        //EfEntityRepositoryBase'i çözümlemem yeterli.


        // EfProductDal : EfEntityRepositoryBase <Product, NorthwindContext>, IProductDal
        // yukardaki satırı yaparak : EfProductDal 'da tüm VT operasyonları hazır hale gelir.
        public List<ProductDetailDto> GetProductDetail()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories   //ürünlerle kategorileri join et
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto       //şu kollonlara görre :
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStoxk = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
