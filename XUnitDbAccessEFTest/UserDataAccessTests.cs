using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Models;
using ChessLogicEntityFramework.OperationObjects;
using System;
using Xunit;
using XUnitDbAccessEFTest.TestingDatabase;

[assembly : CollectionBehavior(DisableTestParallelization = true)]
namespace XUnitDbAccessEFTest
{
    [Collection("SameDbTests")]
    public class UserDataAccessTests
    {
        ITestingDbContext context;
        UserDataAccess userAccess;
        string firstUser,secondUser,thirdUser;

        private void SetupEmpty()
        {
            context = new ChessAppTestingContext();
            userAccess = new UserDataAccess((IDbContext)context);
            firstUser = "firstUser";
            secondUser = "secondUser";
            thirdUser = "thirdUser";

            context.ClearDatabase();
        }

        private void SetupFull()
        {
            context = new ChessAppTestingContext();
            userAccess = new UserDataAccess((IDbContext)context);
            firstUser = "firstUser";
            secondUser = "secondUser";
            thirdUser = "thirdUser";

            context.ClearDatabase();

            userAccess.AddUser(firstUser);
            userAccess.AddUser(secondUser);
            userAccess.AddUser(thirdUser);
        }
        
        [Fact]
        public void AddUser_NewUsers_ReturnsNewUser()
        {
            SetupEmpty();
            context.ClearDatabase();

            bool user1added = userAccess.AddUser(firstUser);
            bool user2added = userAccess.AddUser(secondUser);
            bool user3added = userAccess.AddUser(thirdUser);

            Assert.True(user1added);
            Assert.True(user2added);
            Assert.True(user3added);
        }

        [Fact]
        public void GetUser_NewUsers_ReturnsNewUser() 
        {
            SetupFull();

            User user1 = userAccess.GetUser(firstUser);
            User user2 = userAccess.GetUser(secondUser);
            User user3 = userAccess.GetUser(thirdUser);

            Assert.Equal(firstUser, user1.Name);
            Assert.Equal(1, user1.ID);

            Assert.Equal(secondUser, user2.Name);
            Assert.Equal(2, user2.ID);

            Assert.Equal(thirdUser, user3.Name);
            Assert.Equal(3, user3.ID);
        }

        [Fact]
        public void RemoveUser_NewUsers_ReturnsTrueAndGetReturnsNull()
        {
            SetupFull();

            bool user1Removed = userAccess.RemoveUser(1);
            bool user2Removed = userAccess.RemoveUser(2);
            bool user3Removed = userAccess.RemoveUser(3);

            User isUser1 = userAccess.GetUser(1);
            User isUser2 = userAccess.GetUser(2);
            User isUser3 = userAccess.GetUser(3);

            Assert.True(user1Removed);
            Assert.True(user2Removed);
            Assert.True(user3Removed);

            Assert.Null(isUser1);
            Assert.Null(isUser2);
            Assert.Null(isUser3);
        }

    }
}
