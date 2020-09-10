using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IApiService
    {
        Task<T> GetApiData<T>(string uri) where T : class;
    }
}
