using Domain.Entities;
using Service.Activities;
using System.Runtime.Caching;

namespace Infrastructure.Activities
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly MemoryCache memoryCache;

        public ActivityRepository()
        {
            if (memoryCache == null)
                memoryCache = new MemoryCache("__Infrastructure_MemoryCache");
        }

        public Activity? GetActivity(string key)
        {
            return memoryCache.Contains(key)
                ? (Activity)memoryCache.Get(key)
                : null;
        }

        public void CreateOrUpdateActivity(Activity activity)
        {
            var cacheItemPolicy = new CacheItemPolicy()
            {
                SlidingExpiration = TimeSpan.FromHours(12)
            };

            memoryCache.Set(activity.Key.ToString(), activity, cacheItemPolicy);
        }
    }
}