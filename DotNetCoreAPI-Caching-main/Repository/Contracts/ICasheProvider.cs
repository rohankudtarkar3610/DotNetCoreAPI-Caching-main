using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo7.Repository.Contracts
{
    public interface ICacheProvider
    {
        Task<IEnumerable<ItemMaster>> GetCachedResponse();
        //Task<IEnumerable<ItemMaster>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore);
        T GetData<T>(string key);
        void SetData<T>(string key, T value, TimeSpan expiration);

        object removeData(string key);

    }
}
