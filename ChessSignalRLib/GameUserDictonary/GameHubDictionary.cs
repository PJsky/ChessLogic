using ChessSignalRLibrary.WSModels;
using System;
using System.Collections.Generic;
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
        public static List<ConnectedUserGroup> Get() => connectionList;
    }
}
