using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages  //sabit old için static verildi.
    {
        public static string ProductAdded = "Ürün eklendi..";
        public static string ProductNameInvalid = "Ürün ismi geçesizdir.."; //basit bir değişken olmasına rağmen büyük harf ile yazdım. public old için pascalCase!
        internal static string ProductListed;
        internal static List<Product> MaintenanceTime;

        public static string ProductCounOfCategoryError = "bir kategoride en fazla 110 ürün olabilir";
        public static string ProductNameAlreadyExists ="bu isimde zaten başka bir ürün var";

        public static string CatgoryLimitedExists = "kategori limiti aşıldığı için yeni ürün eklenemiyor !";
    }
}
