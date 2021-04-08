using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key); //generic olmayan versiyon
        void Add(string key, object value, int duration); //duration saat , dk cinsinden ne istersek..

        //cache de var mı
        bool IsAdd(string key);
        void Remove(string key);

        //ismi category olanları uçur
        void RemoveByPattern(string pattern);
    }
}
