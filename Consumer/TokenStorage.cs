using Microsoft.Extensions.Caching.Memory;

namespace Consumer
{
    public class TokenStorage : ITokenStorage
    {
        private readonly IMemoryCache _memoryCache;
        public TokenStorage(IMemoryCache cache)
        {
            _memoryCache = cache;
        }
        public string Get()
        {
            var token = _memoryCache.Get("access_token");
            if (token == null) return string.Empty;
            return token.ToString();
        }

        public void Set(string value)
        {
            _memoryCache.Set("access_token", value);
        }
    }
}