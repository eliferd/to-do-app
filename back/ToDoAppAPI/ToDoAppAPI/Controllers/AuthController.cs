using Microsoft.AspNetCore.Mvc;
using ToDoAppAPI.Database;
using ToDoAppAPI.Helpers;
using ToDoAppAPI.Models;
using ToDoAppAPI.Repositories.Users;
using ToDoAppAPI.RequestModels;
using ToDoAppAPI.ResponseModels;

namespace ToDoAppAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IConfiguration _cfg;
        private JwtManager _jwtManager;
        public AuthController(IConfiguration cfg)
        {
            _cfg = cfg;
            _userRepository = new UserRepository(new DatabaseContext());
            _jwtManager = new JwtManager(cfg);
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
        public ActionResult<AuthResponse> LoginUser([FromBody] LoginRequest requestInput)
        {
            AuthResponse res = new AuthResponse();
            
            try
            {
                User user = _userRepository.GetUser(requestInput.Username, requestInput.Password);
                
                res.Username = user.Username;
                res.Id = user.Id;
                res.Token = _jwtManager.CreateJWT(user);
            }
            catch (Exception ex)
            {
                return Forbid(ex.Message);
            }

            return Ok(res);
        }

        // POST api/auth/signup
        [HttpPost("logout")]
        public void LogoutUser()
        {

        }
    }
}
