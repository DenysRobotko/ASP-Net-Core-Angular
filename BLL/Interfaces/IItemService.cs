using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface to work with items DTO
    /// </summary>
    public interface IItemService
    {
        /// <summary>
        /// Return all items asynchronicity
        /// </summary>
        /// <returns></returns>
        Task<List<ItemDTO>> GetAllItemsAsync();
        /// <summary>
        /// Add single item
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns></returns>
        Task AddItem(ItemDTO item);
        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        Task DeleteItem(int id);
        /// <summary>
        /// Returns all items by name asynchronicity
        /// </summary>
        /// <param name="name">Item name</param>
        /// <returns></returns>
        Task<List<ItemDTO>> GetAllItemsByName(string name);
        /// <summary>
        /// Update item asynchronicity
        /// </summary>
        /// <param name="item">Item to update</param>
        /// <returns></returns>
        Task UpdateAsync(ItemDTO item);
        /// <summary>
        /// Add plus 1 to rate
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        Task AddRate(int id);
    }
}
