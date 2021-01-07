using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;

namespace ChessLogicEntityFramework.OperationObjects
{
    public interface IUserDataAccess
    {
        User AddUser(User user, string password);
        User GetUser(int id);
        User GetUser(string userName);
        List<User> GetUsers(Func<User, bool> filter = null);
        bool RemoveUser(int userId);
        bool RemoveUser(string userName);
        bool RemoveUser(User userToDelete);
    }
}