using ChessLogicEntityFramework.OperationObjects;
using ChessWebApiWithSockets.ViewModels.UserModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebApiWithSockets.Helpers
{
    public class UserGetter
    {
        private IUserDataAccess userDataAccess;
        public UserGetter(IUserDataAccess UserDataAccess)
        {
            userDataAccess = UserDataAccess;
        }
        public UserPresentationModel GetUserFromClaims(HttpContext context)
        {
            string userID = context.User.Identity.Name;
            if (userID == null) return null;

            UserPresentationModel user = new UserPresentationModel();
            user.Name = userDataAccess.GetUser(Int32.Parse(userID)).Name;

            return user;
        }
    }
}
