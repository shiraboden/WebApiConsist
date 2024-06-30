using Microsoft.AspNetCore.Mvc;
using WebApiConsist.Models;
using WebApiConsist.Repositories;

namespace WebApiConsist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser( string userName, string userPassword)
        {
            var createdUser = await _userRepository.CreateUserAsync(userName, userPassword);
            if(createdUser != null)
              return Ok(createdUser);
            return BadRequest("user name or password is already exsists");
        }

        [HttpPost("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userRepository.DeleteUserAsync(userId);
            if (!result)
                return BadRequest("User not found");
            return Ok("User deleted");
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidaeUser([FromBody] User newUser)
        {
            var isValidate = await _userRepository.ValidateUserAsync(newUser.UserName, newUser.UserPassword);
            return Ok("Is validated = " + isValidate);
        }
        [HttpGet("GetUsers")]
       public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
