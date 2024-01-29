using DataAccess.Models;
using Demo7.Repository.Contracts;
using Demo7.Repository.Implementations;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo7.Repository.Implementations
{
    public class CacheProvider : ICacheProvider
    {
        private static readonly SemaphoreSlim GetUsersSemaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        private readonly IItemMasterRepository _itemMasterRepository;
        //private ObjectCache _memoryCache = MemoryCache.Default;

        public CacheProvider(IMemoryCache memoryCache, IItemMasterRepository itemMasterRepository)
        {
            _cache = memoryCache;
            _itemMasterRepository = itemMasterRepository;
        }

        

        public async Task<IEnumerable<ItemMaster>> GetCachedResponse()
        {
            try
            {
                //if (_cache.TryGetValue(ItemsKey, out IEnumerable<ItemMaster> Items))
                //{
                //    return Items;
                //}

                //var Items1 = await _itemMasterRepository.GetAllList();

                //var cacheEntryOptions = new MemoryCacheEntryOptions()
                //    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                //    .SetPriority(CacheItemPriority.Normal)
                //    .SetSize(1024);
                //_cache.Set(ItemsKey, Items1, cacheEntryOptions);

                //return Items1;

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  T GetData<T>(string key)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public object removeData(string key)
        {
            throw new NotImplementedException();
        }

        public void SetData<T>(string key, T value, TimeSpan expiration)
        {
            throw new NotImplementedException();
        }

        //private async Task<IEnumerable<ItemMaster>> GetCachedResponse(string cacheKey, SemaphoreSlim semaphore)
        //{
        //    bool isAvaiable = _cache.TryGetValue(cacheKey, out List<ItemMaster> itemlst);
        //    if (isAvaiable) return itemlst;
        //    try
        //    {
        //        await semaphore.WaitAsync();
        //        isAvaiable = _cache.TryGetValue(cacheKey, out itemlst);
        //        if (isAvaiable) return itemlst;
        //        itemlst = await _itemMasterRepository.GetAllList();
        //        var cacheEntryOptions = new MemoryCacheEntryOptions
        //        {
        //            AbsoluteExpiration = DateTime.Now.AddMinutes(5),
        //            SlidingExpiration = TimeSpan.FromMinutes(2),
        //            Size = 1024,
        //        };
        //        _cache.Set(cacheKey, itemlst, cacheEntryOptions);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        semaphore.Release();
        //    }
        //    return itemlst;
        //}

    }
}

