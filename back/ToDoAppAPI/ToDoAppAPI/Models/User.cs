using System.Text.Json.Serialization;

namespace ToDoAppAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }
    }
}
