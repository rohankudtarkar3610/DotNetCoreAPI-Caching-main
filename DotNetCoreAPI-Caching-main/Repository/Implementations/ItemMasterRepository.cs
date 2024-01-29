using DataAccess.Data;
using DataAccess.Models;
using Demo7.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Demo7.Repository.Implementations
{
    public class ItemMasterRepository : GenericRepository<ItemMaster>, IItemMasterRepository
    {
        private readonly PanditClinicContext _panditClinicContext;

        public ItemMasterRepository(PanditClinicContext panditClinicContext) : base(panditClinicContext)
        {
            _panditClinicContext = panditClinicContext;
        }
        public async Task<List<ItemMaster>> GetAllList()
        {
            try
            {
                var lstitem = await GetListByCondition(x => x.Active == true, x => x.ItemName, true);
                return lstitem.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<ItemMaster> GetById(int id)
        {
            try
            {
                var obj = await GetByCondition(b => b.Id == id);
                return obj ;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemMaster> SaveItem(ItemMaster item)
        {
            try
            {
                var obj = await Insert(item);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ItemMaster> UpdateItem(ItemMaster item)
        {
            try
            {
                var obj = await Update(item);
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
