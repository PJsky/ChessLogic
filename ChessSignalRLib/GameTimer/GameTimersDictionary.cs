using ChessSignalRLibrary.GameHubObjects;
using ChessSignalRLibrary.WSModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSignalRLibrary.GameTimer
{
    public static class GameTimersDictionary
    {
        public static Dictionary<int, GameTimers> gameTimersDictionary = new Dictionary<int, GameTimers>();
        public static void Add(int groupID, string whiteWinner, string blackWinner)
        {
            gameTimersDictionary.Add(groupID, new GameTimers(30, 0, groupID, whiteWinner, blackWinner));
        }

    }
}
