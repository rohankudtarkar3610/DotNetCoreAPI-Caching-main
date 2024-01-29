using DataAccess.Models;

namespace Demo7.Repository.Contracts
{
    public interface IItemMasterRepository : IGenericRepository<ItemMaster>
    {
        Task<List<ItemMaster>> GetAllList();
        Task<ItemMaster> GetById(int id);
        Task<ItemMaster> SaveItem(ItemMaster branch);
        Task<ItemMaster> UpdateItem(ItemMaster branch);
    }
}
