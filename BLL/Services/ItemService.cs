using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    /// <summary>
    /// Interface to work with items DTO
    /// </summary>
    public class ItemService : IItemService
    {
        readonly IRepository<Item> _itemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemRepository"></param>
        /// <param name="mapper"></param>
        public ItemService(IRepository<Item> itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return all items asynchronicity
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemDTO>> GetAllItemsAsync()
        {
            var res = _mapper.Map<List<ItemDTO>>(await _itemRepository.GetAll());
            return res;
        }

        /// <summary>
        /// Add single item
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns></returns>
        public async Task AddItem(ItemDTO item)
        {
            if (item == null)
                throw new ArgumentException("Item must be not null", nameof(item));
            await _itemRepository.AddAsync(_mapper.Map<ItemDTO, Item>(item));

        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        public async Task DeleteItem(int id)
        {
            await _itemRepository.DeleteByAsync(x => x.ItemId == id);
        }

        /// <summary>
        /// Returns all items by name asynchronicity
        /// </summary>
        /// <param name="name">Item name</param>
        /// <returns></returns>
        public async Task<List<ItemDTO>> GetAllItemsByName(string name)
        {
            return _mapper.Map<List<Item>, List<ItemDTO>>(await _itemRepository.GetAllByAsync(x => x.Name == name));
        }

        /// <summary>
        /// Update item asynchronicity
        /// </summary>
        /// <param name="item">Item to update</param>
        /// <returns></returns>
        public async Task UpdateAsync(ItemDTO item)
        {
            await _itemRepository.Update(_mapper.Map<ItemDTO, Item>(item));
        }

        /// <summary>
        /// Add plus 1 to rate
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        public async Task AddRate(int id)
        {
            var res = 0;
            int currentRate = _itemRepository.GetByAsync(x => x.ItemId == id).Result.Rate;
            res = currentRate + 1;
            var item = _itemRepository.GetByAsync(x => x.ItemId == id);
            item.Result.Rate = res;
            await _itemRepository.Update(item.Result);
        }
    }
}