using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// DB context to access to the database
    /// </summary>
    public class PhotoAlbunDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public PhotoAlbunDbContext(DbContextOptions<PhotoAlbunDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
