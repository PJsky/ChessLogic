using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Helpers;
using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicEntityFramework.OperationObjects
{
    public class UserDataAccess : IUserDataAccess
    {
        readonly IDbContext context;
        private PasswordHasher pHasher = new PasswordHasher();
        public UserDataAccess()
        {
            context = new ChessAppContext();
        }

        public UserDataAccess(IDbContext Context)
        {
            context = Context;
        }

        public User GetUser(int id) => context.Users.Find(id);

        public User GetUser(string userName) => context.Users.Where(u => u.Name == userName).FirstOrDefault();

        public List<User> GetUsers(Func<User, bool> filter = null) => context.Users.Where(filter).ToList();


        public User AddUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Password cannot be null or whitespace only");
            if (context.Users.Any(u => u.Name == user.Name)) throw new Exception("Username already taken");

            byte[] passwordHash, passwordSalt;
            pHasher.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public bool RemoveUser(string userName)
        {
            User userToDelete = GetUser(userName);
            return RemoveUser(userToDelete);
        }

        public bool RemoveUser(int userId)
        {
            User userToDelete = GetUser(userId);
            return RemoveUser(userToDelete);
        }

        public bool RemoveUser(User userToDelete)
        {
            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool MakeFriends(User sender, User receiver)
        {
            Friendship friendship = new Friendship();
            //Sender
            friendship.User1ID = sender.ID;
            //Responder
            friendship.User2ID = receiver.ID;

            if (context.Friendships.Any(f => (f.User1ID == friendship.User1ID && f.User2ID == friendship.User2ID) ||
                                        (f.User2ID == friendship.User1ID && f.User1ID == friendship.User2ID)))
                return false;
            context.Friendships.Add(friendship);
            context.SaveChanges();
            return true;
        }

        public bool RespondToFriendAdd(User sender, User receiver, bool isAccepted)
        {

            Friendship friendship = context.Friendships.Where(f => f.User1ID == sender.ID && f.User2ID == receiver.ID).FirstOrDefault();
            if (friendship == null) return false;
            friendship.isAccepted = isAccepted;
            context.SaveChanges();
            return true;
        }

        public bool RemoveFriend(User friendOne, User friendTwo)
        {
            var friendships = context.Friendships.Where(f => (f.User1ID == friendOne.ID && f.User2ID == friendTwo.ID) ||
                                                       (f.User1ID == friendTwo.ID && f.User2ID == friendOne.ID))
                                                       .ToList();
            if (friendships.Count <= 0) return false;
            context.Friendships.RemoveRange(friendships);
            context.SaveChanges();
            return true;
        }

        public List<Friendship> GetAllFriends(User person)
        {
            var personsFriends = context.Friendships.Where(f => f.User1ID == person.ID || f.User2ID == person.ID).ToList();
            return personsFriends;

        }

        public List<Friendship> GetAllFriends(int personID)
        {
            var personsFriends = context.Friendships.Where(f => f.User1ID == personID || f.User2ID == personID).ToList();
            return personsFriends;
        }

    }
}
