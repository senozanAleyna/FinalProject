using Core.Utilities.Ioc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //adapter pattern:var olan Bir şeyi kendi sistemime uyarlıyorum
        IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration)); //ne kadar süre verirsen cachede o kadar kalacak
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);//ben sadece bool istiyorum data istemiyorum
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        //ona verdiğmiz patterna göre silme işlemi yapacaktır
        //çalışma anında bellekten silmeye yarar. çalışma anında müdahale etmeye yarar (reflection)
        public void RemoveByPattern(string pattern)
        {
            //git belleğe bak, cache entriesCollection da dataları tutar onu bul
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //tanımı memorycache olanları bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            //her bir cache elamanını gez
            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            //pattern oluşturuldu
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //şu kurala uyanlar varsa
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            //onları tek tek gzeiyorum keylerini bulup bellekten uçuruyorum
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }

    }
}
