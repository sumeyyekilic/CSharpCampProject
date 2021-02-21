using Microsoft.EntityFrameworkCore;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //db tabloları ile proje class'larını bağlıyoruz.
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //OnConfiguring : senin projen hangi vt ile ilişkili'yi belirteceğim yer!!
            //optionsBuilder.UseSqlServer();//sql server kul. belirtttim
            //Trusted_Connection=true güçlü sistemlerde doğru domain yönetimiyle de böyle kullanılır, domain yönetimi zayıfsa kullanıcı adı şifre girilir burada.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }

        //hangi nesnem hangi nesneye karışık gelecek : bunu Dbset ile yaparız.
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
