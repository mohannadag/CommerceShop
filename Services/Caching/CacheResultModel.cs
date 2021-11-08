using System;
using System.Collections.Generic;
using System.Text;

namespace Commerce.Services.Caching
{
    public class CacheResultModel
    {
        public enum CacheStatus
        {
            resultPending,      // 0
            cached,             // 1
            deleted,            // 2
            doesNotExist,       // 3
            exist,              // 4
            error               // 5
        }

        public CacheResultModel(string key)
        {
            cacheKey = key;
            cacheValue = null;
            cacheStatus = CacheStatus.resultPending;
            error = null;
        }

        public string cacheKey { get; set; }
        public object cacheValue { get; set; }
        public CacheStatus cacheStatus { get; set; }
        internal Exception error;
    }
}
