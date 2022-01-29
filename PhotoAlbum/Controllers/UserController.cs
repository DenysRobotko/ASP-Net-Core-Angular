using BLL;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Controllers
{
    /// <summary>
    /// Controller to work with users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Show list of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            return await _userService.GetAllUsersAsync();
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="userDTO">User to add</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task Register([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.RegisterUser(userDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Log in to the system
        /// </summary>
        /// <param name="userDTO">User object</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid client request");
            }
            UserDTO userToCheck = _userService.AuthenticateUser(userDTO).Result;
            if (userToCheck != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDTO.Login),
                    new Claim(ClaimTypes.Role, userToCheck.Role)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44338",
                    audience: "http://localhost:44338",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
                return Unauthorized();
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="userDTO">User to update</param>
        /// <returns></returns>
        [HttpPut("update"), Authorize(Roles = Roles.Admin)]
        public async Task Update([FromBody] UserDTO userDTO)
        {
            try
            {
                await _userService.UpdateAsync(userDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User to delete</param>
        /// <returns></returns>
        [HttpDelete("delete"), Authorize(Roles = Roles.Admin)]
        public async Task Delete(int id)
        {
            try
            {
                await _userService.DeleteItem(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
