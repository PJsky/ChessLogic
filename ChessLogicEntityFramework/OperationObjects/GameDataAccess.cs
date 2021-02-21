using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicEntityFramework.OperationObjects
{
    public class GameDataAccess : IGameDataAccess
    {
        readonly IDbContext context;
        public GameDataAccess()
        {
            context = new ChessAppContext();
        }

        public GameDataAccess(IDbContext Context)
        {
            context = Context;
        }

        public Game GetGame(int id)
        {
            Game Game = context.Games.Find(id);
            if (Game == null) return Game;
            Game.PlayerBlack = context.Users.Where(u => u.ID == Game.PlayerBlackID).FirstOrDefault();
            Game.PlayerWhite = context.Users.Where(u => u.ID == Game.PlayerWhiteID).FirstOrDefault();
            return Game;
        }

        public List<Game> GetGames(Func<Game, bool> filter = null)
        {
            List<Game> GameList = context.Games.ToList();
            if (filter != null)
                GameList = GameList.Where(filter).ToList();
            GameList = GameList.Select(g =>
            {
                if (g.PlayerWhiteID != null) g.PlayerWhite = context.Users.Find(g.PlayerWhiteID);
                if (g.PlayerBlackID != null) g.PlayerBlack = context.Users.Find(g.PlayerBlackID);
                return g;
            }).ToList();
            return GameList;
        }

        public List<Game> GetUserGames(int userID, Func<Game,bool> filter = null)
        {
            var usersGamesIDs = context.UserGames.Where(ug => ug.UserID == userID).Select(ug => ug.GameID);
            List<Game> UsersGames = context.Games.Where(g => usersGamesIDs.Contains(g.ID)).ToList();
            if (filter != null)
                UsersGames = UsersGames.Where(filter).ToList();
            return UsersGames;
        }

        public int AddGame(User playerWhite, User playerBlack)
        {
            return AddGame(playerWhite, playerBlack, 40, 10);
        }

        public int AddGame(User playerWhite, User playerBlack, int gameTime, int timeGain)
        {
            Game newGame = new Game();
            newGame.PlayerWhite = playerWhite;
            newGame.PlayerBlack = playerBlack;
            newGame.GameTime = gameTime;
            newGame.TimeGain = timeGain;
            context.Games.Add(newGame);
            context.SaveChanges();
            return newGame.ID;
        }

        public bool RemoveGame(int GameId)
        {
            Game GameToDelete = GetGame(GameId);
            return RemoveGame(GameToDelete);
        }

        public bool RemoveGame(Game GameToDelete)
        {
            if (GameToDelete != null)
            {
                context.Games.Remove(GameToDelete);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangePlayers(int gameID, User playerWhite, User playerBlack)
        {
            Game game = GetGame(gameID);

            if (game == null) return false;

            game.PlayerWhite = playerWhite;
            game.PlayerBlack = playerBlack;
            context.SaveChanges();
            return true;
        }

        public bool UpdateMoves(int gameID, string Moves)
        {
            Game game = GetGame(gameID);

            if (game == null) return false;

            if (game.MovesList == null || game.MovesList.Length == 0)
                game.StartedDate = DateTime.Now;

            game.MovesList = Moves;
            context.SaveChanges();
            return true;
        }

        public bool DecideWinner(int gameID, User winner)
        {
            Game game = GetGame(gameID);
            
            if (game == null) return false;

            game.Winner = winner;
            context.SaveChanges();
            return true;
        }

        public bool DecideWinner(int gameID, int userID)
        {
            User user = context.Users.Find(userID);

            return DecideWinner(gameID, user);
        }

        public bool FinishGame(int gameID)
        {
            Game game = GetGame(gameID);

            if (game == null) return false;
            game.IsFinished = true;
            game.FinishedDate = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public bool AddMovesToList(int gameID, string Move)
        {
            Game game = GetGame(gameID);

            if (game == null) return false;

            if (game.MovesList == null || game.MovesList.Length == 0)
                game.StartedDate = DateTime.Now;

            game.MovesList += Move;
            context.SaveChanges();
            return true;
        }

        public bool SaveGame(int userID,int gameID)
        {
            if (context.UserGames.Any(ug => ug.UserID == userID && ug.GameID == gameID)) return false;
            UserGames userGame = new UserGames();
            userGame.UserID = userID;
            userGame.GameID = gameID;
            context.UserGames.Add(userGame);
            context.SaveChanges();
            return true;
        }
    }
}
