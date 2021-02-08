using ChessLogicEntityFramework.OperationObjects;
using ChessSignalRLibrary.GameHubObjects;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace ChessSignalRLibrary.WSModels
{
    public class GameTimers
    {
        Stopwatch stopwatchWhite = new Stopwatch();
        Stopwatch stopwatchBlack = new Stopwatch();
        int whiteSecondsLeft, blackSecondsLeft;
        Timer endTimer = new Timer();
        int timeGain;
        string whiteWinner,blackWinner;
        int groupID;
        private bool isFirstTurn = true;
        //Action<int, string> decideWinner;
        IHubContext<GameHub> hubContext = GameHub.GlobalContext;


        public int WhiteTime
        {
            get { return whiteSecondsLeft - (int)(stopwatchWhite.ElapsedMilliseconds/1000); }
        }
        public int BlackTime
        {
            get { return blackSecondsLeft - (int)(stopwatchBlack.ElapsedMilliseconds / 1000); }
        }


        public GameTimers()
        {
            whiteSecondsLeft = 600;
            blackSecondsLeft = 600;
            timeGain = 0;
        }
        public GameTimers(int GameTime, int TimeGain = 0,int GroupID = 0, string WhiteWinner = null, string BlackWinner = null)
        {
            whiteSecondsLeft = GameTime;
            blackSecondsLeft = GameTime;
            timeGain = TimeGain;
            groupID = GroupID;
            whiteWinner = WhiteWinner;
            blackWinner = BlackWinner;
        }

        public void StartGame()
        {
            stopwatchWhite.Start(); 
            endTimer.Close();
            endTimer = new Timer(BlackTime * 1000);
            endTimer.Start();
            endTimer.Enabled = true;
            endTimer.Elapsed += EndOnTimeOnWhite;
        }
        public void ChangeTurn()
        {
            if (stopwatchWhite.IsRunning)
            {
                //End turn and add time to white
                stopwatchWhite.Stop();
                if(!isFirstTurn)
                    whiteSecondsLeft += timeGain;
                isFirstTurn = false;
                stopwatchBlack.Start();

                //Release used timer resources and create a new one
                endTimer.Close();
                endTimer = new Timer(BlackTime*1000);
                endTimer.Start();
                endTimer.Enabled = true;
                endTimer.Elapsed += EndOnTimeOnBlack;
            }else if (stopwatchBlack.IsRunning)
            {
                //End turn and add time to black
                stopwatchBlack.Stop();
                blackSecondsLeft += timeGain;
                stopwatchWhite.Start();

                //Release used timer resources and create a new one
                endTimer.Close();
                endTimer = new Timer(WhiteTime*1000);
                endTimer.Start();
                endTimer.Enabled = true;
                endTimer.Elapsed += EndOnTimeOnWhite;
            }
        }

        private void EndOnTimeOnWhite(Object source, ElapsedEventArgs e)
        {
            //if (decideWinner.Method != null)
            //    decideWinner(groupID, blackWinner);
            //Console.WriteLine("Game ended on white turn");
            hubContext.Clients.Groups("gameRoom_" + groupID).SendAsync("ReceiveWinner",   new { winner = blackWinner});
            GameDataAccess gameDataAccess = new GameDataAccess();
            int winnerID = (int)gameDataAccess.GetGame(groupID).PlayerBlackID;
            gameDataAccess.DecideWinner(groupID, winnerID);
            gameDataAccess.FinishGame(groupID);
            endTimer.Stop();
            endTimer.Close();
        }

        private void EndOnTimeOnBlack(Object source, ElapsedEventArgs e)
        {
            //if (decideWinner.Method != null)
            //    decideWinner(groupID, whiteWinner);
            //Console.WriteLine("Game has ended on black turn");

            hubContext.Clients.Groups("gameRoom_" + groupID).SendAsync("ReceiveWinner", new { winner = whiteWinner });
            GameDataAccess gameDataAccess = new GameDataAccess();
            int winnerID = (int)gameDataAccess.GetGame(groupID).PlayerWhiteID;
            gameDataAccess.DecideWinner(groupID, winnerID);
            gameDataAccess.FinishGame(groupID);
            endTimer.Stop();
            endTimer.Close();
        }

        public void CloseGame()
        {
            endTimer.Enabled = false;
            endTimer.AutoReset = false;
            endTimer.Stop();
            endTimer.Close();
        }
    }
}
