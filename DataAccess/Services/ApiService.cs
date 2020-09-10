using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class ApiService : IApiService
    {

        private readonly IKeyVault KeyVault;
        private readonly IRedisCache RedisCache;
        private readonly ILogger Logger;
        private HttpClient client { get; set; }

        public ApiService(IKeyVault _KeyVault, IRedisCache _RedisCache, ILogger<ApiService> _Logger)
        {
            KeyVault = _KeyVault;
            RedisCache = _RedisCache;
            Logger = _Logger;
        }

        //Retrieving data from api
        private async Task<T>CallApi<T>(string uri) where T:class
        {

            if (client == null) client = new HttpClient();
            
            var response = await client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content.StringToObject<T>();
        }

        //Getting data from redis or api
        public async Task<T>GetApiData<T>(string uri) where T: class
        {
            try
            {
                var redis = await RedisCache.RetrieveData<T>(uri);

                //Check if data already exists in redis
                if (redis != null)
                {
                    Logger.LogInformation($"Data found on rediscache for URL {uri}");
                    return redis;
                }

                //Displaying message about data not found on redis
                Logger.LogInformation($"Data not found on rediscache for URL {uri}");

                //Getting base url to call
                var secret = await KeyVault.GetSecretAsync("ApiClientBaseUri");

                //Calling api to get the data
                var data = await CallApi<T>($"{secret.Value}{uri}");

                //Inserting data into redis
                await RedisCache.InsertData(uri, data);

                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
