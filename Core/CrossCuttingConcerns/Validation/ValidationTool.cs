using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validater, object entity)
        {
            //kodu evrensel olarak kullanılabilir hale getircem
            var context = new ValidationContext<object>(entity);
            var result = validater.Validate(context);
            if (!result.IsValid) //sonuc geçersizse
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
