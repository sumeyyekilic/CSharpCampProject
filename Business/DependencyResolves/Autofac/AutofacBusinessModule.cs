using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolves.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<IProductDal>().As<EfProductDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//çalışan uy içerisinde

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()  //implemente edilmiş interface leri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() //onlar için çağır
                }).SingleInstance();
        }
    }
}
