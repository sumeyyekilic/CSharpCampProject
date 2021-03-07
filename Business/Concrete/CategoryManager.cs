using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal; //bağımlığımı constructure injection ile yapıyorum..

        //generate const. seçeneği ile:
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        //iş kodlarını yazabilmem için ICategoryService ikinci kez implamente istiyor.
        public IDataResult<List<Category>> GetAll()
        {
            //yeni bir iş kuralı gelirse auth. gibi buraya yazarız ve vurası her yeri etkiler.
            //bu yuzden dalı enjecte etmiyoruz. her entity nin kendi servisi olacak.

            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>( _categoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
