using System.Security.Cryptography;
using ToDoAppAPI.Database;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _dbCtx;
        private bool disposed = false;
        private int hashIterations = 5000;

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
            User? user = _dbCtx.Users.Where(x => x.Username == username).Select(usr => usr).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            byte[] inputPassword = Rfc2898DeriveBytes.Pbkdf2(password, Convert.FromBase64String(user.PasswordSalt), this.hashIterations, HashAlgorithmName.SHA512, 256);
            string convertedPassword = Convert.ToBase64String(inputPassword);
            if (string.Equals(convertedPassword, user.Password))
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

            user.PasswordSalt = Convert.ToBase64String(salt);

            user.Password = Convert.ToBase64String(Rfc2898DeriveBytes.Pbkdf2(user.Password, Convert.FromBase64String(user.PasswordSalt), this.hashIterations, HashAlgorithmName.SHA512, 256));
            _dbCtx.Users.Add(user);

            _dbCtx.SaveChanges();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
