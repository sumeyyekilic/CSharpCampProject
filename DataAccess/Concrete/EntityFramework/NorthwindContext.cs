using Microsoft.EntityFrameworkCore;
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
        }
    }
}
