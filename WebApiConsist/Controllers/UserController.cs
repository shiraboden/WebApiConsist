using Microsoft.AspNetCore.Mvc;
using WebApiConsist.Models;
using WebApiConsist.Repositories;
using Microsoft.Extensions.Logging;

namespace WebApiConsist.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger= logger;
            _logger.LogDebug(1, "NLog injected into UserController");
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            _logger.LogInformation($"try to create a new user with the parameters:({newUser.UserName}, {newUser.UserPassword}))");
            var createdUser = await _userRepository.CreateUserAsync(newUser.UserName, newUser.UserPassword);
            if (createdUser != null) 
            {
                _logger.LogInformation($"created a new user:({createdUser.UserId}, {newUser.UserName}, {newUser.UserPassword})");
                return Ok(createdUser);
            }
            else
            {
                _logger.LogInformation("can not create a new user");
                return BadRequest("user name or password is already exsists");

            }
        }

        [HttpPost("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            _logger.LogInformation("try to delete an user with id=" + userId);
            var result = await _userRepository.DeleteUserAsync(userId);
            if (!result)
            {
                _logger.LogError("can not find user for deleting");
                return BadRequest("User is not found");
            }
            else
            {
                _logger.LogInformation("userId = " + userId + " is deleted");
                return Ok("User deleted");
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidaeUser([FromBody] User newUser)
        {
            _logger.LogInformation($"try to validate  new user with the parameters:{newUser.UserName},{newUser.UserPassword}");
            var isValidate = await _userRepository.ValidateUserAsync(newUser.UserName, newUser.UserPassword);
            return Ok("Is validated = " + isValidate);
        }

    }
}
