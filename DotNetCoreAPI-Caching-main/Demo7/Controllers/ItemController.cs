using Microsoft.AspNetCore.Mvc;
using Demo7.Repository.Contracts;
using GoldLoanRebirth.Model;
using Microsoft.Extensions.Logging;
using DataAccess.Models;
using Demo7.Model.Model;
using Demo7.Repository.Implementations;

using FluentValidation.AspNetCore;
using FluentValidation.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Demo7.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class ItemController : ControllerBase
    {
        private const string ItemsKey = "Itemslst";
        private const string ItemsKeyRedis = "ItemsRedis";
        private readonly IItemMasterRepository _itemMasterRepository;
        private readonly ILogger<ItemController> logger;
        public IValidator<UpdateItem> _validatorUpdate;
        private IValidator<saveItem> _validatorSave;
        //private ICacheProvider _cacheProvider;

        private IMemoryCache _cache;
        private IDistributedCache distributedCached;
        public ItemController(ILogger<ItemController> _logger,
            IItemMasterRepository itemMasterRepository,
            IValidator<saveItem> validatorSave,
            IValidator<UpdateItem> validatorUpdate
            //, ICacheProvider cacheProvider
            , IMemoryCache cache
            , IDistributedCache rcache
            )
        {
            logger = _logger;
            _itemMasterRepository = itemMasterRepository;
            _validatorSave = validatorSave;
            _validatorUpdate = validatorUpdate;
            //_cacheProvider = cacheProvider;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            distributedCached = rcache ?? throw new ArgumentNullException(nameof(rcache));
        }

        //[HttpPost]
        //[Route("GetItemList")]
        //public async Task<ResponseMessage> getItemList()
        //{
        //    ResponseMessage response = new();
        //    List<ItemMaster> itemlst = new();
        //    try
        //    {
        //        // If found in cache, return cached data
        //        if (_memoryCache.TryGetValue(getAllItemListKey, out itemlst))
        //        {
        //            itemlst=itemlst;
        //        }
        //        response.message = "Success";
        //        response.result = true;
        //        return response;

        //        var itemlst = await _itemMasterRepository.GetAllList();

        //        response.message = "Success";
        //        response.result = true;
        //        response.data = itemlst;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logger.LogError(ex.Message.ToString());
        //        response.result = false;
        //        response.message = "Error in Get All Branch";
        //        response.data = null;
        //    }
        //    return response;
        //}


        //[HttpPost]
        //[Route("GetItemList")]
        //public async Task<ResponseMessage> getItemList()
        //{
        //    ResponseMessage response = new();
        //    try
        //    {
        //        var employees = _cacheProvider.GetCachedResponse().Result;
        //        response.message = "Success";
        //        response.result = true;
        //        response.data = employees;
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        [HttpPost]
        [Route("GetById")]
        public async Task<ResponseMessage> getById(int id)
        {
            ResponseMessage response = new();
            try
            {
                ItemMaster item = await _itemMasterRepository.GetById(id);
                response.message = "Success";
                response.result = true;
                response.data = item;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message.ToString());
                response.result = false;
                response.message = "Error in Get item";
                response.data = null;
            }
            return response;
        }

        [HttpPost]
        [Route("GetList")]
        public async Task<ResponseMessage> getList()
        {
            ResponseMessage response = new();
            try
            {
                List<ItemMaster> item = await _itemMasterRepository.GetAllList();
                response.message = "Success";
                response.result = true;
                response.data = item;
            }
            catch (Exception ex)
            {
                //logger.LogError(ex.Message.ToString());
                response.result = false;
                response.message = "Error in Get List ";
                response.data = null;
            }
            return response;
        }



        /// <summary>
        /// Save new Item
        /// </summary>
        /// <param name="saveItemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveItem")]
        public async Task<ResponseMessage> SaveItem(saveItem saveItemModel)
        {
            ResponseMessage response = new();
            try
            {
                ValidationResult result = await _validatorSave.ValidateAsync(saveItemModel);
                if (!result.IsValid)
                {
                    #region Copy the validation results into ModelState & ASP.NET uses the ModelState collection to populate 
                    result.AddToModelState(this.ModelState);
                    #endregion

                    response.result = false;
                    response.message = result.Errors.Select(p => p.ErrorMessage).ToList();
                }
                else
                {
                    ItemMaster item = new()
                    {
                        ItemName = saveItemModel.ItemName,
                        IsRateApplicable = saveItemModel.IsRateApplicable,
                        Rate = saveItemModel.IsRateApplicable == true ? saveItemModel.Rate : 0,
                    };
                    await _itemMasterRepository.SaveItem(item);
                    response.result = true;
                    response.message = "Record Inserted Successfully!";
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                response.result = false;
                response.message = "Error in Save Item";
                response.data = null;
            }
            return response;
        }



        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="UpdateItemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateItem")]
        public async Task<ResponseMessage> UpdateItem(UpdateItem updateItem)
        {
            ResponseMessage response = new();
            try
            {
                ValidationResult result = await _validatorUpdate.ValidateAsync(updateItem);
                if (!result.IsValid)
                {
                    #region Copy the validation results into ModelState & ASP.NET uses the ModelState collection to populate 
                    result.AddToModelState(this.ModelState);
                    #endregion

                    response.result = false;
                    response.message = result.Errors.Select(p => p.ErrorMessage).ToList();
                }
                else
                {
                    ItemMaster item = new()
                    {
                        Id = updateItem.Id,
                        ItemName = updateItem.ItemName,
                        IsRateApplicable = updateItem.IsRateApplicable,
                        Rate = updateItem.IsRateApplicable == true ? updateItem.Rate : 0,
                    };
                    await _itemMasterRepository.UpdateItem(item);
                    response.result = true;
                    response.message = "Record Updated Successfully!";
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                response.result = false;
                response.message = "Error in Update Item";
                response.data = null;
            }
            return response;
        }



        //[HttpPost]
        //[Route("GetListFromCache")]
        //public async Task<IActionResult> getfromCache()
        //{
        //    ResponseMessage response = new();
        //    try
        //    {
        //        var cacheItems = _cacheProvider.Get<IEnumerable<ItemMaster>>("itemList");

        //        if (cacheItems != null && cacheItems.Count()>0)
        //        {
        //            return Ok(cacheItems);
        //        }

        //        var Items = await _itemMasterRepository.GetAllList();

        //        var expiryTime=DateTimeOffset.Now.AddMinutes(2);
        //        _cacheProvider.Set<IEnumerable<ItemMaster>>("itemList",Items, expiryTime);

        //        return Ok(Items);

        //        //List<ItemMaster> item = await _itemMasterRepository.GetAllList();
        //        //response.message = "Success";
        //        //response.result = true;
        //        //response.data = item;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logger.LogError(ex.Message.ToString());
        //        //response.result = false;
        //        //response.message = "Error in Get List ";
        //        //response.data = null;
        //    }
        //    //return response;
        //}



        [HttpPost]
        [Route("GetListFromCache")]
        public async Task<IActionResult> getfromCache()
        {
            ResponseMessage response = new();
            try
            {
                if (_cache.TryGetValue(ItemsKey, out IEnumerable<ItemMaster> Items))
                {
                    return Ok(Items);
                }
               
                var ItemsList = await _itemMasterRepository.GetAllList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(15))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                _cache.Set(ItemsKey, ItemsList, cacheEntryOptions);

                return Ok(ItemsList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("GetFromRedisCache")]
        public async Task<IActionResult> GetfromRedisCache()
        {
            ResponseMessage response = new();
            try
            {
                string serializeobj;
                List<ItemMaster> itemList;

                var Items = await distributedCached.GetAsync(ItemsKeyRedis);

                if (Items != null)
                {
                    serializeobj = Encoding.UTF8.GetString(Items);
                    itemList = JsonConvert.DeserializeObject<List<ItemMaster>>(serializeobj);
                }
                else
                {
                    itemList = await _itemMasterRepository.GetAllList();

                    serializeobj = JsonConvert.SerializeObject(itemList);
                    Items = Encoding.UTF8.GetBytes(serializeobj);
                    var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(15));
                    await distributedCached.SetAsync(ItemsKeyRedis, Items, cacheEntryOptions);
                }
                return Ok(itemList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }
}
