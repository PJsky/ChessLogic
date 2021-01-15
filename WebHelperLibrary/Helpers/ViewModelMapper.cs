using ChessLogicEntityFramework.Models;
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
            GamePresentationModel gameModel = new GamePresentationModel()
            {
                GameID = game.ID,
                PlayerWhite = game.PlayerWhite == null ? null : game.PlayerWhite.Name,
                PlayerBlack = game.PlayerBlack == null ? null : game.PlayerBlack.Name
            };
            return gameModel;
        }
    }
}
