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
            return Game;
        }

        public List<Game> GetGames(Func<Game, bool> filter = null)
        {
            List<Game> GameList = context.Games.Where(filter).ToList();
            return GameList;
        }

        public bool AddGame(User playerWhite, User playerBlack)
        {
            Game newGame = new Game();
            newGame.PlayerWhite = playerWhite;
            newGame.PlayerBlack = playerBlack;
            var Game = context.Games.Add(newGame);
            context.SaveChanges();
            return true;
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

            game.MovesList = Moves;
            context.SaveChanges();
            return true;
        }

        public bool AddMovesToList(int gameID, string Move)
        {
            Game game = GetGame(gameID);

            if (game == null) return false;

            game.MovesList += Move;
            context.SaveChanges();
            return true;
        }
    }
}
