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

        [HttpGet("getGame/{id}")]
        public IActionResult GetGame(int? id)
        {
            if(id == null) return BadRequest("No such game found");
            var game = gameDataAccess.GetGame((int)id);
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
            var user = userGetter.GetUserFromClaims(HttpContext);
            User gamePlayer = null;
            List<Game> games = new List<Game>();
            if (user != null)
            {
                gamePlayer = userDataAccess.GetUser(user.UserID);
                games = gameDataAccess.GetGames(g => g.Winner == null && (
                                                g.PlayerBlackID == null || g.PlayerWhiteID == null ||
                                                g.PlayerWhiteID == gamePlayer.ID || g.PlayerBlackID == gamePlayer.ID));
            }
            else if(user == null)
                games= gameDataAccess.GetGames(g => g.PlayerBlackID == null || g.PlayerWhiteID == null);

            var gamesModels = games.Select(g => ViewModelMapper.MapGameToPresentation(g));
            return Ok(gamesModels);
        }

        [HttpGet("getMatchHistory")]
        public IActionResult GetMatchHistory()
        {
            var user = userGetter.GetUserFromClaims(HttpContext);
            if (user == null) return BadRequest("U are not logged in");
            List<Game> games = new List<Game>();
            User gamePlayer = userDataAccess.GetUser(user.UserID);
            games = gameDataAccess.GetGames(g => (g.PlayerWhiteID == gamePlayer.ID || g.PlayerBlackID == gamePlayer.ID) && g.WinnerID != null);

            var gamesModels = games.Select(g => ViewModelMapper.MapGameToPresentation(g));
            return Ok(gamesModels);
        }


        [HttpPost("addGame")]
        public IActionResult AddGame()
        {
            var user = userGetter.GetUserFromClaims(HttpContext);
            User gamePlayer = userDataAccess.GetUser(user.UserID);

            var game = gameDataAccess.AddGame(gamePlayer, null);
            return Ok(new { message = "new game has been created", gameID =  game});
        }

        [HttpPost("joinGame")]
        public IActionResult JoinGame([FromBody] GameToFindModel gameToFindModel)
        {
            var game = gameDataAccess.GetGame(gameToFindModel.GameID);
            var user = userGetter.GetUserFromClaims(HttpContext);
            User gamePlayer = userDataAccess.GetUser(user.UserID);

            if (gamePlayer.ID == game.PlayerBlackID || gamePlayer.ID == game.PlayerWhiteID) return Ok("You have already joined the game");
            if (game.PlayerWhiteID != null && game.PlayerBlackID != null) return BadRequest("Both seats in the game are taken");
            //if(game.PlayerWhiteID == null) 
            //{ 
            //    gameDataAccess.ChangePlayers(game.ID, gamePlayer, game.PlayerBlack);
            //}else if(game.PlayerBlackID == null)
            //{
            //    gameDataAccess.ChangePlayers(game.ID, game.PlayerWhite, gamePlayer);
            //}

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
