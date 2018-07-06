using System;
using System.Runtime.Caching;

namespace Utility
{
    public interface IMemoryCacher
    {
        object GetValue(string key);
        bool Add(string key, object value, DateTimeOffset? absExpiration);
        void Delete(string key);
    }

    public class MemoryCacher : IMemoryCacher
    {

        public object GetValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(key);
        }

        public bool Add(string key, object value, DateTimeOffset? absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            bool result;

            if (absExpiration.HasValue)
                result = memoryCache.Add(key, value, absExpiration.Value);
            else
                result = memoryCache.Add(key, value, null);

            return result;
        }

        public void Delete(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }
    }
}
