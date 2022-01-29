using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// Implements UnitOfWork pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        //Dbcontext
        private readonly PhotoAlbunDbContext _context;
        private UserRepository _userRepository;

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="connectionString"></param>
        public UnitOfWork(DbContextOptions<PhotoAlbunDbContext> connectionString)
        {
            _context = new PhotoAlbunDbContext(connectionString);
        }
        
        public IUserRepository UserRepository
        {
            get
            {
                if(_userRepository==null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}