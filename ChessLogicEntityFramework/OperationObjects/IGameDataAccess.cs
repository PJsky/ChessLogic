using ChessLogicEntityFramework.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;

namespace ChessLogicEntityFramework.OperationObjects
{
    public interface IGameDataAccess
    {
        int AddGame(User playerWhite, User playerBlack);
        Game GetGame(int id);
        List<Game> GetGames(Func<Game, bool> filter = null);
        List<Game> GetUserGames(int userID, Func<Game, bool> filter = null);
        bool RemoveGame(Game GameToDelete);
        bool RemoveGame(int GameId);
        bool ChangePlayers(int gameID, User playerWhite, User playerBlack);
        bool UpdateMoves(int gameID, string Moves);
        bool AddMovesToList(int gameID, string Move);
        bool SaveGame(int userID, int gameID);
        bool DecideWinner(int gameID, User winner);
        bool FinishGame(int gameID);

    }
}