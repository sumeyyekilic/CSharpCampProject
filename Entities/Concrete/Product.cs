using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product :IEntity
    {
        //public "bu class'a diğer katmanlar da ulaşabilsin" demek 
        //çünkü data access ürünü ekleyecek
        //business ürünü kontrol edecek
        //console ürünü gösterecek.
        //entities bu üç katmanı kullanacağı için burayı public yapmamızınn sebebi bu


        //bir classın defoult'ı internal'dır. internal sadece entities erşebilir demek.

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
