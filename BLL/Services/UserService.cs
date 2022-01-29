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
    /// Interface to work with users DTO
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        public async Task RegisterUser(UserDTO user)
        {
            if (user == null)
                throw new ArgumentException("Passed data was null", nameof(user));

            await _userRepository.AddAsync(new User()
            {
                Login = user.Login,
                Password = user.Password,
                Role = Roles.User
            });
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="user">User object</param>
        /// <returns></returns>
        public async Task<UserDTO> AuthenticateUser(UserDTO user)
        {
            var userCheck = _mapper.Map<User, UserDTO>(await _userRepository.GetByAsync(x => x.Login==user.Login && x.Password==user.Password));

            if (userCheck == null)
                return null;

            return userCheck;
        }

        /// <summary>
        /// Return all users asynchronicity
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var res = _mapper.Map<List<UserDTO>>(await _userRepository.GetAll());
            return res;
        }

        /// <summary>
        /// Update user asynchronicity
        /// </summary>
        /// <param name="item">User object</param>
        /// <returns></returns>
        public async Task UpdateAsync(UserDTO item)
        {
            await _userRepository.Update(_mapper.Map<UserDTO, User>(item));
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        public async Task DeleteItem(int id)
        {
            await _userRepository.DeleteByAsync(x => x.Id == id);
        }
    }
}
