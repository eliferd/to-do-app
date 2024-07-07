using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Users
{
    public interface IUserRepository
    {
        User? GetUser(string username, string password);
        User? GetUser(Guid userId);
        void InsertUser(User user);
        void DeleteUser(Guid userId);
        void Save();
    }
}
