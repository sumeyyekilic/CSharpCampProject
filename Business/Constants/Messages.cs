﻿using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Constants
{
    public static class Messages  //sabit old için static verildi.
    {
        public static string ProductAdded = "Ürün eklend..";
        public static string ProductNameInvalid = "Ürün ismi geçesizdir.."; //basit bir değişken olmasına rağmen büyük harf ile yazdım. public old için pascalCase!
        internal static string ProductListed;
        internal static List<Product> MaintenanceTime;
    }
}
