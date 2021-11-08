using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Services.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache cache;

        public CachingService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        // TODO here I have to make changes on the sync methods to match the async methods (change string type with T).

        #region Storing
        /// <summary>
        /// Save item/s to the cache
        /// </summary>
        /// <param name="key">The key for the cache</param>
        /// <param name="expHours">Expiration time in hours</param>
        /// <param name="expMinutes">Expiration time in minutes</param>
        /// <param name="expSeconds">Expiration time in seconds</param>
        /// <param name="objectToCache">the items to Cache</param>
        /// <param name="forceOverWrite">force save even if exist</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the items that saved to the cache
        /// </returns>
        public async Task<CacheResultModel> SaveToCacheAsync<T>(
            string key, int expHours, int expMinutes, int expSeconds, T objectToCache, bool forceOverWrite)
        {
            CacheResultModel result = new CacheResultModel(key);

            try
            {
                T cachedObject;
                await Task.Run(() =>
               {
                   if (!cache.TryGetValue<T>(key, out cachedObject))
                   {
                       // if the key can't be found
                       TimeSpan time = new TimeSpan(expHours, expMinutes, expSeconds);
                       //cachedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                       cachedObject = objectToCache;
                       cache.Set<T>(key, cachedObject, time);
                       result.cacheValue = cachedObject;
                       result.cacheStatus = CacheResultModel.CacheStatus.cached;
                   }
                   // if the key does exist in cache memory
                   else
                   {
                       if (!forceOverWrite)
                       {
                           result.cacheStatus = CacheResultModel.CacheStatus.exist;
                       }
                       else
                       {
                           TimeSpan time = new TimeSpan(expHours, expMinutes, expSeconds);
                           //cachedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                           cachedObject = objectToCache;
                           cache.Set<T>(key, cachedObject, time);
                           result.cacheValue = cachedObject;
                           result.cacheStatus = CacheResultModel.CacheStatus.cached;
                       }
                   }
               });
            }
            catch (Exception error)
            {
                result.cacheStatus = CacheResultModel.CacheStatus.error;
                result.error = error;

            }

            return result;
        }


        public CacheResultModel SaveToCache<T>(
            string key, int expHours, int expMinutes, int expSeconds, T objectToCache, bool forceOverWrite)
        {
            CacheResultModel result = new CacheResultModel(key);

            try
            {
                object cachedObject = null;
                if (!cache.TryGetValue<object>(key, out cachedObject))
                {
                    // if the key can't be found
                    TimeSpan time = new TimeSpan(expHours, expMinutes, expSeconds);
                    //cachedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                    cachedObject = objectToCache;
                    cache.Set<object>(key, cachedObject, time);
                    result.cacheValue = cachedObject;
                    result.cacheStatus = CacheResultModel.CacheStatus.cached;
                }
                // if the key does exist in cache memory
                else
                {
                    if (!forceOverWrite)
                    {
                        result.cacheStatus = CacheResultModel.CacheStatus.exist;
                    }
                    else
                    {
                        TimeSpan time = new TimeSpan(expHours, expMinutes, expSeconds);
                        //cachedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                        cachedObject = objectToCache;
                        cache.Set<object>(key, cachedObject, time);
                        result.cacheValue = cachedObject;
                        result.cacheStatus = CacheResultModel.CacheStatus.cached;
                    }
                }
            }
            catch (Exception error)
            {
                result.cacheStatus = CacheResultModel.CacheStatus.error;
                result.error = error;

            }

            return result;
        }
        #endregion

        #region Retreive

        public async Task<CacheResultModel> RetreiveFromCacheAsync(string key)
        {
            CacheResultModel result = new CacheResultModel(key);

            try
            {
                object cachedObject;
                await Task.Run(() =>
                {
                    if (!cache.TryGetValue<object>(key, out cachedObject))
                    {
                        result.cacheStatus = CacheResultModel.CacheStatus.doesNotExist;
                    }
                    else
                    {
                        result.cacheStatus = CacheResultModel.CacheStatus.exist;
                        result.cacheValue = cachedObject;
                    }
                });
            }
            catch (Exception error)
            {
                result.cacheStatus = CacheResultModel.CacheStatus.error;
                result.error = error;

            }

            return result;
        }

        public CacheResultModel RetreiveFromCache(string key)
        {
            CacheResultModel result = new CacheResultModel(key);

            try
            {
                string cachedObject = string.Empty;

                if (!cache.TryGetValue<string>(key, out cachedObject))
                {
                    result.cacheStatus = CacheResultModel.CacheStatus.doesNotExist;
                }
                else
                {
                    result.cacheStatus = CacheResultModel.CacheStatus.exist;
                    result.cacheValue = cachedObject;
                }
            }
            catch (Exception error)
            {
                result.cacheStatus = CacheResultModel.CacheStatus.error;
                result.error = error;

            }

            return result;
        }

        #endregion

        #region Deleting

        public async Task<CacheResultModel> DeleteFromCacheAsync(string key)
        {
            CacheResultModel result = new CacheResultModel(key);
            try
            {
                object cachedObject;
                await Task.Run(() =>
                {
                    if (!cache.TryGetValue<object>(key, out cachedObject))
                    {
                        result.cacheStatus = CacheResultModel.CacheStatus.doesNotExist;
                    }
                    else
                    {
                        cache.Remove(key);
                        result.cacheStatus = CacheResultModel.CacheStatus.deleted;
                    }
                });
            }
            catch (Exception error)
            {
                result.cacheStatus = CacheResultModel.CacheStatus.error;
                result.error = error;

            }

            return result;
        }

        public CacheResultModel DeleteFromCache(string key)
        {
            CacheResultModel result = new CacheResultModel(key);
            try
            {
                string cachedObject = string.Empty;

                if (!cache.TryGetValue<string>(key, out cachedObject))
                {
                    result.cacheStatus = CacheResultModel.CacheStatus.doesNotExist;
                }
                else
                {
                    cache.Remove(key);
                    result.cacheStatus = CacheResultModel.CacheStatus.deleted;
                }
            }
            catch (Exception error)
            {
                result.cacheStatus = CacheResultModel.CacheStatus.error;
                result.error = error;

            }

            return result;
        }

        #endregion
    }
}
