using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key,object value,int duration);//anahtar,gelecek data ve cacheda kalma süresi
        bool IsAdd(string key);//cacheden mi getirelim veritabanınadn mı cachede var mı yok mu kontrol edicez
        void Remove(string key);
        void RemoveByPattern(string pattern);

    }
}
