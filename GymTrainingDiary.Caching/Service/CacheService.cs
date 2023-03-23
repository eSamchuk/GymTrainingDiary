using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace GymTrainingDiary.Caching.Service
{
    public class CacheService : ICacheService
    {
        public IDistributedCache cache;

        public CacheService(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task PutInCacheAsync(string key, object content, TimeSpan timeToLive)
        {
            if (content == null) return;

            await this.cache.SetStringAsync(key, JsonSerializer.Serialize(content), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = timeToLive });
        }

        public async Task<string> ReadFromCacheAsync(string key)
        {
            var response = await this.cache.GetStringAsync(key);
            return string.IsNullOrEmpty(response) ? null : response;
        }
    }
}
