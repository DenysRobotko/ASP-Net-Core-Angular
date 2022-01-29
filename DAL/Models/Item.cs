using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    /// <summary>
    /// Describe Item table in database
    /// </summary>
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public DateTime Published { get; set; }
        public string ImgPath { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int Rate { get; set; }
        public User User { get; set; }
    }
}
