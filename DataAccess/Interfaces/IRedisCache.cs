using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRedisCache
    {
        //Retrieving data from redis cache
        Task<T> RetrieveData<T>(string Key) where T : class;
        //Inserting data into redis cache
        Task InsertData<T>(string key, T data) where T : class;
    }
}
