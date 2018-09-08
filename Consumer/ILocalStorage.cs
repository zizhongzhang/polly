using Microsoft.Extensions.Caching.Memory;

namespace Consumer
{
    public interface IStorage
    {
        T Get<T>(string key);
        void Set<T>(string key, T item);
    }

    class LocalStorage : IStorage
    {
        private readonly IMemoryCache _memoryCache;
        public LocalStorage(IMemoryCache memoryCache)
        {

            _memoryCache = memoryCache;
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T item)
        {
            _memoryCache.Set(key, item);
        }
    }
}