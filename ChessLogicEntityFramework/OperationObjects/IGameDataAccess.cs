using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;

namespace ChessLogicEntityFramework.OperationObjects
{
    public interface IGameDataAccess
    {
        bool AddGame(User playerWhite, User playerBlack);
        Game GetGame(int id);
        List<Game> GetGames(Func<Game, bool> filter = null);
        bool RemoveGame(Game GameToDelete);
        bool RemoveGame(int GameId);
    }
}