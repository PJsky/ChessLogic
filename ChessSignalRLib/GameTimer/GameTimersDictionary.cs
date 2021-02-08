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
        public static void Add(int groupID, string whiteWinner, string blackWinner, int gameTime, int timeGain)
        {
            gameTimersDictionary.Add(groupID, new GameTimers(gameTime, timeGain, groupID, whiteWinner, blackWinner));
        }

    }
}
