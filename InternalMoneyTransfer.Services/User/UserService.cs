using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using InternalMoneyTransfer.DAL.Repository.UserRepository;

namespace InternalMoneyTransfer.Services.User
{
    public class UserService : IUserService
    {
        #region Constructor

        public UserService(IRepository<Core.DataModel.User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Fields

        private readonly IRepository<Core.DataModel.User> _userRepository;

        #endregion

        #region Methods

        public Core.DataModel.User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email.Equals(email));
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public Core.DataModel.User GetUserById(int id)
        {
            return _userRepository.Get(id);
        }

        public Core.DataModel.User CreateUser(Core.DataModel.User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            var existUser = _userRepository.GetAll().FirstOrDefault(u => u.Email.Equals(user.Email));
            if (existUser != null)
                throw new ArgumentException("Username is already exists");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userRepository.Insert(user);

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (var i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != storedHash[i])
                        return false;
            }

            return true;
        }

        public IEnumerable<Core.DataModel.User> GetAll()
        {
            return _userRepository.GetAll();
        }

        #endregion
    }
}