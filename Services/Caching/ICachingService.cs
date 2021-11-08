using System.Threading.Tasks;

namespace Commerce.Services.Caching
{
    public interface ICachingService
    {
        CacheResultModel DeleteFromCache(string key);
        Task<CacheResultModel> DeleteFromCacheAsync(string key);
        CacheResultModel RetreiveFromCache(string key);
        Task<CacheResultModel> RetreiveFromCacheAsync(string key);
        CacheResultModel SaveToCache<T>(string key, int expHours, int expMinutes, int expSeconds, T objectToCache, bool forceOverWrite);
        Task<CacheResultModel> SaveToCacheAsync<T>(string key, int expHours, int expMinutes, int expSeconds, T objectToCache, bool forceOverWrite);
    }
}