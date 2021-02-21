using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        //kategori ile ilgili dış dünyaya neyi servis etmek istiyorsam oları yazıyorum.
        List<Category> GetAll();
        Category GetById(int categoryId);
    }
}
