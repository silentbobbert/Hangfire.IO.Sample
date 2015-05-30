using EFCache;
using EFCache.Redis;
using log4net;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Common;

namespace Hangfire.Sample.Repository
{
    public class Configuration : DbConfiguration
    {
        public static ILog Log;
        public Configuration()
        {
            var useRedis = false;
            bool.TryParse(ConfigurationManager.AppSettings["UseRedisCache"], out useRedis);

            if (!useRedis) return;
            try
            {
                var cache = new RedisCache(ConfigurationManager.AppSettings["RedisConnectionString"]);

                cache.CachingFailed += (sender, args) =>
                {
                    if (Log != null)
                    {
                        Log.Error(string.Format("A Caching exception occured: {0}", args.Message), args);
                    }
                };

                var transactionHandler = new CacheTransactionHandler(cache);

                AddInterceptor(transactionHandler);

                Loaded +=
                    (sender, args) => args.ReplaceService<DbProviderServices>(
                        (s, _) => new CachingProviderServices(s, transactionHandler, new RedisCachingPolicy()));
            }
            catch (Exception ex)
            {
                if (Log != null)
                {
                    Log.Error(string.Format("A Caching exception occured: {0}", ex.Message), ex);
                }
            }
            
        }
    }
}
