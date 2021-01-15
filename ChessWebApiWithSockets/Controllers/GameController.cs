using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessLogicEntityFramework.Models;
using ChessLogicEntityFramework.OperationObjects;
using SharedWebObjectsLibrary.Helpers;
using SharedWebObjectsLibrary.ViewModels.GameModels;
using SharedWebObjectsLibrary.ViewModels.GameModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace ChessWebApiWithSockets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private IGameDataAccess gameDataAccess;
        private IUserDataAccess userDataAccess;
        private UserGetter userGetter;
        public GameController(IGameDataAccess GameDataAccess, IUserDataAccess UserDataAccess)
        {
            gameDataAccess = GameDataAccess;
            userDataAccess = UserDataAccess;
            userGetter = new UserGetter(userDataAccess);
        }

        [HttpGet("getGame")]
        public IActionResult GetGame([FromBody] GameToFindModel gameToFindModel)
        {
            var game = gameDataAccess.GetGame(gameToFindModel.GameID);
            if (game == null) return BadRequest("No such game found");

            GamePresentationModel gameModel = ViewModelMapper.MapGameToPresentation(game);

            return Ok(gameModel);
        }

        [HttpGet("getGames")]
        public IActionResult GetGames()
        {
            var games = gameDataAccess.GetGames();
            var gamesModels = games.Select(g => ViewModelMapper.MapGameToPresentation(g));
            return Ok(gamesModels);
        }

        [HttpGet("getFreeGames")]
        public IActionResult GetFreeGames()
        {
            var games = gameDataAccess.GetGames(g => g.PlayerBlackID == null || g.PlayerWhiteID == null);
            var gamesModels = games.Select(g => ViewModelMapper.MapGameToPresentation(g));
            return Ok(gamesModels);
        }


        [HttpPost("addGame")]
        public IActionResult AddGame()
        {
            var user = userGetter.GetUserFromClaims(HttpContext);
            User gamePlayer = userDataAccess.GetUser(user.UserID);

            gameDataAccess.AddGame(gamePlayer, null);
            return Ok(new { message = "new game has been created" });
        }

        [HttpPost("joinGame")]
        public IActionResult JoinGame([FromBody] GameToFindModel gameToFindModel)
        {
            var game = gameDataAccess.GetGame(gameToFindModel.GameID);
            var user = userGetter.GetUserFromClaims(HttpContext);
            User gamePlayer = userDataAccess.GetUser(user.UserID);

            if (gamePlayer.ID == game.PlayerBlackID || gamePlayer.ID == game.PlayerWhiteID) return BadRequest("You have already joined the game");
            if (game.PlayerWhiteID != null && game.PlayerBlackID != null) return BadRequest("Both seats in the game are taken");
            if(game.PlayerWhiteID == null) 
            { 
                gameDataAccess.ChangePlayers(game.ID, gamePlayer, game.PlayerBlack);
            }else if(game.PlayerBlackID == null)
            {
                gameDataAccess.ChangePlayers(game.ID, game.PlayerWhite, gamePlayer);
            }

            GamePresentationModel gameModel = ViewModelMapper.MapGameToPresentation(game);

            return Ok(gameModel);
        }

        [HttpPost("quitGame")]
        public IActionResult QuitGame([FromBody] GameToFindModel gameToFindModel)
        {
            var game = gameDataAccess.GetGame(gameToFindModel.GameID);
            var user = userGetter.GetUserFromClaims(HttpContext);
            User gamePlayer = userDataAccess.GetUser(user.UserID);

            if (game.PlayerWhiteID != gamePlayer.ID && game.PlayerBlackID != gamePlayer.ID) return BadRequest(new { message = "Can't quit a game u are not in" });
            if (game.PlayerWhiteID == gamePlayer.ID)
            {
                gameDataAccess.ChangePlayers(game.ID, null, game.PlayerBlack);
            }else if(game.PlayerBlackID == gamePlayer.ID)
            {
                gameDataAccess.ChangePlayers(game.ID, game.PlayerWhite, null);
            }

            GamePresentationModel gameModel = ViewModelMapper.MapGameToPresentation(game);

            return Ok(gameModel);
        }
    }
}
