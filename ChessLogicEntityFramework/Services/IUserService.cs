using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicEntityFramework.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        User Create(User user, string password);
        User GetById(int id);
    };
}
