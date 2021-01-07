using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicEntityFramework.Services
{
    public interface IUserAuthenticator
    {
        User Authenticate(string email, string password);

    };
}
