using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Describe user repository
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly PhotoAlbunDbContext _context;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">DB context</param>
        public UserRepository(PhotoAlbunDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns user by id with detail
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        public async Task<User> GetByIdWithDetails(int id)
        {
            var item = _context.Users.Include(x => x.Items).FirstOrDefaultAsync(f => f.Id == id);
            return await item;
        }
    }
}
