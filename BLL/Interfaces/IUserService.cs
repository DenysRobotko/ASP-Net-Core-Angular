using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface to work with users DTO
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        Task RegisterUser(UserDTO user);
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        Task<UserDTO> AuthenticateUser(UserDTO user);
        /// <summary>
        /// Return all users asynchronicity
        /// </summary>
        /// <returns></returns>
        Task<List<UserDTO>> GetAllUsersAsync();
        /// <summary>
        /// Update user asynchronicity
        /// </summary>
        /// <param name="item">User object</param>
        /// <returns></returns>
        Task UpdateAsync(UserDTO item);
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Task DeleteItem(int id);
    }
}
