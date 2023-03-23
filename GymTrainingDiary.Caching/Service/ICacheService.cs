using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainingDiary.Caching.Service
{
    public interface ICacheService
    {
        Task PutInCacheAsync(string key, object content, TimeSpan timeToLive);

        Task<string> ReadFromCacheAsync(string key);
    }
}
