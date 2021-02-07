﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Çıplak Class Kalmasın
    //IEntity usingi eklenir. buna işaretleme denir.
    //Ientity görürsek VT tablosudur
    //işaretleme avantajı:Category'nin referansını tutabiliyor
    public class Category : IEntity  
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

