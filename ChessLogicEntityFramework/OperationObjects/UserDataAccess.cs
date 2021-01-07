using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Helpers;
using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicEntityFramework.OperationObjects
{
    public class UserDataAccess
    {
        readonly IDbContext context;
        private PasswordHasher pHasher = new PasswordHasher();
        public UserDataAccess()
        {
            context = new ChessAppContext();
        }

        public UserDataAccess(IDbContext Context)
        {
            context = Context;
        }

        public User GetUser(int id) => context.Users.Find(id);

        public User GetUser(string userName) => context.Users.Where(u => u.Name == userName).FirstOrDefault();

        public List<User> GetUsers(Func<User, bool> filter = null) => context.Users.Where(filter).ToList();


        public User AddUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Password cannot be null or whitespace only");
            if (context.Users.Any(u => u.Name == user.Name)) throw new Exception("Username already taken");

            byte[] passwordHash, passwordSalt;
            pHasher.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public bool RemoveUser(string userName)
        {
            User userToDelete = GetUser(userName);
            return RemoveUser(userToDelete);
        }

        public bool RemoveUser(int userId)
        {
            User userToDelete = GetUser(userId);
            return RemoveUser(userToDelete);
        }

        public bool RemoveUser(User userToDelete)
        {
            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
