using ChessLogicEntityFramework.Models;
using ChessLogicEntityFramework.OperationObjects;
using SharedWebObjectsLibrary.ViewModels.GameModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedWebObjectsLibrary.Helpers
{
    public static class ViewModelMapper
    {
        public static GamePresentationModel MapGameToPresentation(Game game)
        {
            UserDataAccess userDataAccess = new UserDataAccess();
            GamePresentationModel gameModel = new GamePresentationModel()
            {
                GameID = game.ID,
                PlayerWhite = game.PlayerWhiteID == null ? null : userDataAccess.GetUser((int)game.PlayerWhiteID).Name,
                PlayerBlack = game.PlayerBlackID == null ? null : userDataAccess.GetUser((int)game.PlayerBlackID).Name
            };
            return gameModel;
        }
    }
}
