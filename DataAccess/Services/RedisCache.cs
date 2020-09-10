using DataAccess.Interfaces;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class RedisCache : IRedisCache
    {

        private readonly IKeyVault keyVault;
        private readonly ILogger Logger;
        private IDatabase redisCache { get; set; }

        public RedisCache(IKeyVault _KeyVault,ILogger<RedisCache> _Logger)
        {
            keyVault = _KeyVault;
            Logger = _Logger;
        }

        //Initializing redis cache
        public async Task Init()
        {
            SecretBundle connectionString = await keyVault.GetSecretAsync("RedisCacheCon");
            redisCache = ConnectionMultiplexer.Connect(connectionString.Value).GetDatabase();
        }

        public async Task InsertData<T>(string key, T data) where T : class
        {
            //Initializing redisCache if null
            if (redisCache == null) await Init();

            //Storing value in redis cache
            await redisCache.StringSetAsync(key, data.ObjectToString());

            Logger.LogInformation($"Saving data into redis cache for key {key}");

        }

        public async Task<T> RetrieveData<T>(string Key) where T : class
        {
            if (redisCache == null) await Init();

            //Gets string value from key
            string data = await redisCache.StringGetAsync(Key);

            return
                data != null ? data.StringToObject<T>() : null;

        }
    }
}
