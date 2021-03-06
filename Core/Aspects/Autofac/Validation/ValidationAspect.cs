using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;  //attribute de type işle geçmek zorundayız
        public ValidationAspect(Type validatorType) //bana validator type ver diyor
        {
            //defensive code : savunma odaklı kodlama
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatör type IValidator değilse o zaman kız! dior
            {
                throw new System.Exception("bu bir doğrulama sınıf değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {   //MethodInterception de bu yapı var ama burada override ediyorum !  //productValidator
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //Activator.CreateInstance bir instance oluşturur. //product tipi
            //prod val 'ın base metodunki argumanların 0.tipini yakala
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  //reflaction kodu. 
            //metodun argumanlarını gez. invocation(add metodu).
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);  //validation ın tipine eşit olan parametreleri git bul diyor. birden fazla olabilir
            //eğer ordaki bir tip benim entity type'ıma (product türü) eşitse onları validate et
            foreach (var entity in entities) //tüm params tek tek gez
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
