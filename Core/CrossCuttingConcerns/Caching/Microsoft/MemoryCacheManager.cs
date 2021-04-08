using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    //BURADA .net core dan gelen kodu kendime uyarlıyorum
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern  : adaptasyon deseni, bir şeyi kendi sistemime uyarlıyrorum. (var olan sistemi). diyorum ki ben sana göre çalışmayacağım, sen benim sistemime göre çalışacaksın..
        
        IMemoryCache _memoryCache; // microsoftun kendi kütüphanesi //memory cache kullanarak işlemlerimizi yazpıcaz

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();  //IMemoryCache in karşılığını ver diyoruz.
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(10)); //cache anahtarı, değeri ve duration'ı verdim.  expire olma(bellekten ne zaman uçuracağımı) giriyorum.
                                                                    //TimeSpan.FromMinutes(10) cache de kalma süresi
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key); // _memoryCache den get et
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            //BELLEKTE BÖYLE BİR CACHE DEĞERİ VAR MI ?
            return _memoryCache.TryGetValue(key, out _);  //(key, out _); BEN SADECE BELLEKTE BÖYLE BİR ANAHTAR VAR MI YOK MU ONU ÖĞRENMEK İSTİYORUM,
                                                          //Datayı istemiyorum.
                                                          //zahmet olmasın diye cache deki değeri verme demenin karşılığı c# da budur. 
        }
        public void Remove(string key)
        {
            _memoryCache.Remove(key);  //burada bz direk aspectin içinde
        }

        //RemoveByPattern :Çalışma anında bellekten silmeyi sağlar.
        public void RemoveByPattern(string pattern)  
        {   //ona verdiğimiz bir pattern'a göre silme işlemi yapacak

            //Reflection: elimizde bir sınıfın instance'i var bellekte ve ona çalışma anında müdahale etmek istiyoruz. Bunu Reflection ile yapıyorduk
            //çalışma anında elimizde bulunan nesnelere ve olmayanlarıda yeniden oluşturmak gibi çalışmalar yapabilileceğimiz bir yapıdır.
            //Kısaca kodu çalışma anında oluşturma, çalışma anında müdahale etme gibi şeyleri ""reflaction"" ile yaparız.

            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);  //Gidip  belleğe bak ;MemoryCache türünde olan EntriesCollection'ı bul. bunu .net dökümantasyınunda bellekte nasıl tuttuğuna dair bilgi veriyor
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;   //Definition'ı _memoryCache olanları bul.
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection) //her bir cache elemanını gez.
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            //Pattern'ı bu şekilde oluşturuyoruz
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);  //her bir cache elemanından bu kurala uyanlar 
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList(); //silme işlemini gerçekleştirirken vereceğim, değerin kendisi(regex) olacak.

            //yukardaki cache datası içindeki alanlardan benim gönderdiğim değere uygun olanlr varsa(keysToRemove)  onları : keysToRemove türü içine  atacak ve foreach ile gezip
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key); //bellekten uçuyorum
            }

            //yukardaki yapıyı ezbere bilmeye gerek yok..
            // .net memory cache in dökumantasyonuna giderseniz görebilrisiniz. 
        }
    }
}
