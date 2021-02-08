using ChessSignalRLibrary.WSModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessSignalRLibrary.GameUserDictonary
{
    public static class GameHubDictionary
    {
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
        public static List<ConnectedUserGroup> connectionList = new List<ConnectedUserGroup>();
        public static void Add(string connectionID, string group)
        {
            dictionary.Add(connectionID, group);
        }
        //public static Dictionary<string, string> Get() => dictionary;
        public static int? GetUsersGame(int userID) 
        {
            var userConnectionToGame = connectionList.Where(u => u.UserID == userID).FirstOrDefault();
            int? GameID = userConnectionToGame == null ? null : (int?)userConnectionToGame.GameRoomID;
            return GameID;
        }
        
        public static List<ConnectedUserGroup> Get() => connectionList;
    }
}
