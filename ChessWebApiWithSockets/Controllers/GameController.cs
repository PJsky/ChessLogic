using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessLogicEntityFramework.OperationObjects;
using ChessWebApiWithSockets.ViewModels.GameModels;
using Microsoft.AspNetCore.Mvc;

namespace ChessWebApiWithSockets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        private IGameDataAccess gameDataAccess;
        public GameController(IGameDataAccess GameDataAccess)
        {
            gameDataAccess = GameDataAccess;
        }

        [HttpPost("addGame")]
        public IActionResult AddGame([FromBody] NewGameModel newGameModel)
        {
            throw new NotImplementedException();
        }
    }
}
