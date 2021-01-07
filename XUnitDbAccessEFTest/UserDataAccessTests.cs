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
        string password;
        User firstUser, secondUser, thirdUser;

        private void SetupEmpty()
        {
            context = new ChessAppTestingContext();
            userAccess = new UserDataAccess((IDbContext)context);

            firstUser = new User();
            firstUser.Name = "firstUser";

            secondUser = new User();
            secondUser.Name = "secondUser";

            thirdUser = new User();
            thirdUser.Name = "thirdUser";

            password = "123123321";

            context.ClearDatabase();
        }

        private void SetupFull()
        {
            context = new ChessAppTestingContext();
            userAccess = new UserDataAccess((IDbContext)context);

            firstUser = new User();
            firstUser.Name = "firstUser";

            secondUser = new User();
            secondUser.Name = "secondUser";

            thirdUser = new User();
            thirdUser.Name = "thirdUser";

            password = "123123321";

            context.ClearDatabase();

            userAccess.AddUser(firstUser, password);
            userAccess.AddUser(secondUser, password);
            userAccess.AddUser(thirdUser, password);
        }
        
        [Fact]
        public void AddUser_NewUsers_ReturnsNewUser()
        {
            SetupEmpty();
            context.ClearDatabase();

            bool user1added = userAccess.AddUser(firstUser, password) != null;
            bool user2added = userAccess.AddUser(secondUser, password) != null;
            bool user3added = userAccess.AddUser(thirdUser, password) != null;

            Assert.True(user1added);
            Assert.True(user2added);
            Assert.True(user3added);
        }

        [Fact]
        public void GetUser_NewUsers_ReturnsNewUser() 
        {
            SetupFull();

            User user1 = userAccess.GetUser(firstUser.Name);
            User user2 = userAccess.GetUser(secondUser.Name);
            User user3 = userAccess.GetUser(thirdUser.Name);

            Assert.Equal(firstUser.Name, user1.Name);
            Assert.Equal(1, user1.ID);

            Assert.Equal(secondUser.Name, user2.Name);
            Assert.Equal(2, user2.ID);

            Assert.Equal(thirdUser.Name, user3.Name);
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
