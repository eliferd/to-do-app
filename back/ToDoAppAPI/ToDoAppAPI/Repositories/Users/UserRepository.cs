using System.Security.Cryptography;
using System.Text;
using ToDoAppAPI.Database;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _dbCtx;
        private bool disposed = false;

        public UserRepository(DatabaseContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public void DeleteUser(Guid userId)
        {
            User? user = _dbCtx.Users.Find(userId);

            if (user != null)
            {
                _dbCtx.Users.Remove(user);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbCtx.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public User? GetUser(string username, string password)
        {
            User? user = _dbCtx.Users.Find(username);

            if (user == null)
            {
                return null;
            }

            byte[] inputPassword = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(user.PasswordSalt), 5000, HashAlgorithmName.SHA512, 256);

            if (Encoding.UTF8.GetString(inputPassword) == user.Password)
            {
                return user;
            } else
            {
                throw new InvalidDataException("Invalid password.");
            }
        }

        public User? GetUser(Guid userId)
        {
            return _dbCtx.Users.Find(userId);
        }

        public void InsertUser(User user)
        {
            Random rand = new Random();
            byte[] salt = new byte[12];

            rand.NextBytes(salt);

            user.PasswordSalt = Encoding.UTF8.GetString(salt);

            user.Password = Encoding.UTF8.GetString(Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(user.Password), salt, 5000, HashAlgorithmName.SHA512, 256));
            _dbCtx.Users.Add(user);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
