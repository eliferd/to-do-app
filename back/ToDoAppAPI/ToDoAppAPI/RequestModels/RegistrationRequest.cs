using System.ComponentModel.DataAnnotations;

namespace ToDoAppAPI.RequestModels
{
    public class RegistrationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
