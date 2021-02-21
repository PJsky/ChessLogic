using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChessLogicEntityFramework.Helpers;
using ChessLogicEntityFramework.Models;
using ChessLogicEntityFramework.OperationObjects;
using ChessLogicEntityFramework.Services;
using SharedWebObjectsLibrary.Helpers;
using SharedWebObjectsLibrary.ViewModels;
using SharedWebObjectsLibrary.ViewModels.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ChessWebApiWithSockets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private IUserAuthenticator userAuthenticator;
        private IUserDataAccess userDataAccess;

        public UserController(IUserAuthenticator UserAuthenticator, IUserDataAccess UserDataAccess)
        {
            userAuthenticator = UserAuthenticator;
            userDataAccess = UserDataAccess;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthUserModel authUser)
        {
            User user = userAuthenticator.Authenticate(authUser.Name, authUser.Password);
            if (user == null) return BadRequest(new { message = "Bad username or password" });

            string token = JWTGenerator.GetToken("cQfTjWnZr4u7x!z%", user.ID.ToString());

            return Ok(new
            {
                ID = user.ID,
                Username = user.Name,
                Token = token
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthUserModel authUser)
        {
            User user = new User();
            user.Name = authUser.Name;
            if (authUser.Name.Length < 6) return BadRequest("Name too short");
            if (authUser.Password.Length < 6) return BadRequest("Password too short");
            try
            {
                userDataAccess.AddUser(user, authUser.Password);
                return Ok(new { message = "User created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUser()
        {
            var x = new UserGetter(userDataAccess);
            var user = x.GetUserFromClaims(HttpContext);

            return Ok(new
            {
                username = user.Name,
                userID = user.UserID
            });
        }

    }
}
