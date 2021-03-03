using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;  //attribute de type işle geçmek zorundayız
        public ValidationAspect(Type validatorType) //bana validator type ver diyor
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen validatör type IValidator değilse o zaman kız! dior
            {
                throw new System.Exception("bu bir doğrulama sınıf değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {   //MethodInterception de bu yapı var ama burada override ediyorum !
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
