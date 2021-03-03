using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>  //class ın attr oku
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);  //ilgili metodun attr oku
            classAttributes.AddRange(methodAttributes); //onları bir listeye koy
                                                        //şu hareket * otomatik olarak sistemdeki tüm metotları loglamaya dahil et. 3 sene proje geliştyse bunu eklemen yeterli. şimdilik loglama altyapım olmadığı içiön kaldırdım.
                                                        //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));  //çalışma sırasına göre de sırala

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
