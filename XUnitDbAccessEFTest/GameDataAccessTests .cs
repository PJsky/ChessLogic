using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Models;
using ChessLogicEntityFramework.OperationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using XUnitDbAccessEFTest.TestingDatabase;

namespace XUnitDbAccessEFTest
{
    [Collection("SameDbTests")]
    public class GameDataAccessTests
    { 
        ITestingDbContext context;
        GameDataAccess gameAccess;
        UserDataAccess userAccess;
        string playerWhite, playerBlack;

        private void SetupEmpty()
        {
            context = new ChessAppTestingContext();
            gameAccess = new GameDataAccess((IDbContext)context);
            userAccess = new UserDataAccess((IDbContext)context);
            playerWhite = "playerWhite";
            playerBlack = "playerBlack";

            context.ClearDatabase();

            userAccess.AddUser(playerWhite);
            userAccess.AddUser(playerBlack);

        }

        private void SetupFull()
        {
            context = new ChessAppTestingContext();
            gameAccess = new GameDataAccess((IDbContext)context);
            userAccess = new UserDataAccess((IDbContext)context);
            playerWhite = "playerWhite";
            playerBlack = "playerBlack";

            context.ClearDatabase();

            userAccess.AddUser(playerWhite);
            userAccess.AddUser(playerBlack);

            User playerW = userAccess.GetUser(playerWhite);
            User playerB = userAccess.GetUser(playerBlack);

            gameAccess.AddGame(playerW, playerB);
        }
        
        [Fact]
        public void AddGame_NewGameWithPlayers_ReturnsTrue()
        {
            SetupEmpty();

            User playerWhite = userAccess.GetUser(1);
            User playerBlack = userAccess.GetUser(2);

            bool result = gameAccess.AddGame(playerWhite, playerBlack);
            
            Assert.True(result);
        }

        [Fact]
        public void GetGame_NewGameWithPlayers_ReturnsNewGame() 
        {
            SetupFull();

            Game game = gameAccess.GetGame(1);

            Assert.Equal(1, game.ID);
            Assert.Equal(playerWhite, game.PlayerWhite.Name);
            Assert.Equal(playerBlack, game.PlayerBlack.Name);
        }

        [Fact]
        public void GetGames_NewGameWithPlayer_ReturnsNewGame()
        {
            SetupFull();

            Game game = gameAccess.GetGames(g => g.ID == 1).FirstOrDefault();
            
            Assert.Equal(1, game.ID);
            Assert.Equal(playerWhite, game.PlayerWhite.Name);
            Assert.Equal(playerBlack, game.PlayerBlack.Name);
        }

        [Fact]
        public void RemoveGame_NewGame_RemovesGameAndGetReturnsNull()
        {
            SetupFull();

            bool isGameRemoved = gameAccess.RemoveGame(1);

            Game game = gameAccess.GetGame(1);

            Assert.True(isGameRemoved);
            Assert.Null(game);
        }

    }
}
