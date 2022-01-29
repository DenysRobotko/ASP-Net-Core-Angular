using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using ContentDispositionHeaderValue = System.Net.Http.Headers.ContentDispositionHeaderValue;
using Microsoft.AspNetCore.Authorization;
using BLL;

namespace PhotoAlbum.Controllers
{
    /// <summary>
    /// Controller to work with items
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemService"></param>
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Return all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> Get()
        {
            return await _itemService.GetAllItemsAsync();
        }

        /// <summary>
        /// Return all items by name
        /// </summary>
        /// <param name="name">Name to search</param>
        /// <returns></returns>
        [HttpGet("/byname")]
        public async Task<IEnumerable<ItemDTO>> GetByName(string name)
        {
            return await _itemService.GetAllItemsByName(name);
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="item">Item object</param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async Task Add(ItemDTO item)
        {
            await _itemService.AddItem(item);
        }

        /// <summary>
        /// Save image to the project folder
        /// </summary>
        /// <returns>Path to saved image</returns>
        [HttpPost, DisableRequestSizeLimit]
        [Route("SaveFile")]
        public IActionResult SaveFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        /// <summary>
        /// Increment rate of the object by 1
        /// </summary>
        /// <param name="item">Item object</param>
        /// <returns></returns>
        [HttpPut("like"), Authorize]
        public  async Task AddRate(ItemDTO item)
        {
            int id = item.ItemId;
            await _itemService.AddRate(id);
        }

        /// <summary>
        /// Delete item from collection
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task DeleteItem(int id)
        {
            try
            {
                await _itemService.DeleteItem(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
