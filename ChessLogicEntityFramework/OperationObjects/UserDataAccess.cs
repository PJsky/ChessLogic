using ChessLogicEntityFramework.DbContextObjects;
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
        public UserDataAccess()
        {
            context = new ChessAppContext();
        }

        public UserDataAccess(IDbContext Context)
        {
            context = Context;
        }

        public User GetUser(int id)
        {
            User user = context.Users.Find(id);
            return user;
        } 
        public User GetUser(string userName)
        {
            User user = context.Users.Where(u => u.Name == userName).FirstOrDefault();
            return user;
        }

        public List<User> GetUsers(Func<User, bool> filter = null)
        {
            List<User> userList = context.Users.Where(filter).ToList();
            return userList;
        }

        public bool AddUser(string userName)
        {
            User newUser = new User();
            newUser.Name = userName;
            if (GetUser(userName) == null)
            { 
                var user = context.Users.Add(newUser);
                context.SaveChanges();
                return true;
            }
            return false;
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
