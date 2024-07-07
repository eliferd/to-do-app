namespace ToDoAppAPI.ResponseModels
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
