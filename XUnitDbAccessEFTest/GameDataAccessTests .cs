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
        User playerWhite, playerBlack;
        string password;

        private void SetupEmpty()
        {
            context = new ChessAppTestingContext();
            gameAccess = new GameDataAccess((IDbContext)context);
            userAccess = new UserDataAccess((IDbContext)context);

            playerWhite = new User();
            playerWhite.Name = "playerWhite";

            playerBlack = new User();
            playerBlack.Name = "playerBlack";

            password = "123123321";

            context.ClearDatabase();

            userAccess.AddUser(playerWhite, password);
            userAccess.AddUser(playerBlack, password);

        }

        private void SetupFull()
        {
            context = new ChessAppTestingContext();
            gameAccess = new GameDataAccess((IDbContext)context);
            userAccess = new UserDataAccess((IDbContext)context);

            playerWhite = new User();
            playerWhite.Name = "playerWhite";

            playerBlack = new User();
            playerBlack.Name = "playerBlack";

            password = "123123321";

            context.ClearDatabase();

            userAccess.AddUser(playerWhite, password);
            userAccess.AddUser(playerBlack, password);

            User playerW = userAccess.GetUser(playerWhite.Name);
            User playerB = userAccess.GetUser(playerBlack.Name);

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
            Assert.Equal(playerWhite.Name, game.PlayerWhite.Name);
            Assert.Equal(playerBlack.Name, game.PlayerBlack.Name);
        }

        [Fact]
        public void GetGames_NewGameWithPlayer_ReturnsNewGame()
        {
            SetupFull();

            Game game = gameAccess.GetGames(g => g.ID == 1).FirstOrDefault();
            
            Assert.Equal(1, game.ID);
            Assert.Equal(playerWhite.Name, game.PlayerWhite.Name);
            Assert.Equal(playerBlack.Name, game.PlayerBlack.Name);
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
