using System;

namespace BLL.DTO
{
    /// <summary>
    /// Data tranfer object for Item model
    /// </summary>
    public class ItemDTO
    {
        public int ItemId { get; set; }

        public DateTime Published { get; set; }
        public string ImgPath { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int? Rate { get; set; }
    }
}
