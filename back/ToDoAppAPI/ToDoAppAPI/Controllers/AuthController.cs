using Microsoft.AspNetCore.Mvc;
using ToDoAppAPI.Database;
using ToDoAppAPI.Models;
using ToDoAppAPI.Repositories.Users;
using ToDoAppAPI.RequestModels;

namespace ToDoAppAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserRepository _userRepository;
        public AuthController()
        {
            _userRepository = new UserRepository(new DatabaseContext());
        }

        // POST api/auth/signup
        [HttpPost("signup")]
        public void SignupUser([FromBody] RegistrationRequest requestInput)
        {
            User user = new User
            {
                Username = requestInput.Username,
                Password = requestInput.Password
            };

            _userRepository.InsertUser(user);
        }

        // POST api/auth/login
        [HttpPost("login")]
        public void LoginUser([FromBody] LoginRequest requestInput)
        {
            _userRepository.GetUser(requestInput.Username, requestInput.Password);
        }

        // POST api/auth/signup
        [HttpPost("logout")]
        public void LogoutUser()
        {
        }
    }
}
