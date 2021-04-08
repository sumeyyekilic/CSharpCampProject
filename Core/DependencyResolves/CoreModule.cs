using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolves
{
    public class CoreModule :ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache(); //dotnet in kendisiinin. dotnet kendisi  **IMemoryCache _memoryCache;** bunu injection yapabiliyor.
            //yani _memoryCache karşılığı var artık.

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //birisi senden ICacheManager  isterse ona MemoryCacheManager(bizim) ver
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            //yarın ben redis cache istersm burada MemoryCacheManager yerine  RedisCacheManager yazmam yeterli.
        }

    }
}
