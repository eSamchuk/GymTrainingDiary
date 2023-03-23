using GymTrainingDiary.Caching;
using GymTrainingDiary.Caching.Service;

namespace GymTrainingDiaryAPI.Setup
{
    public static class CacheConfiguration
    {
        public static void ConfigureRedisCache(this IServiceCollection services, CacheSettings cacheSettings)
        {
            services.AddTransient<ICacheService, CacheService>();
            services.AddSingleton(cacheSettings);
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = cacheSettings.Endpoint;
            });
        }
    }
}
