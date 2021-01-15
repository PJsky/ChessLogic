using System;
using System.Collections.Generic;
using System.Text;

namespace SharedWebHelperLibrary.FromClaimsGetter
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
            var userFromDb = userDataAccess.GetUser(Int32.Parse(userID));
            user.Name = userFromDb.Name;
            user.UserID = userFromDb.ID;
            return user;
        }
    }
}
