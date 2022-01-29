using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    /// <summary>
    /// Describe user repository
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Returns user by id with detail
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        Task<User> GetByIdWithDetails(int id);
    }
}
