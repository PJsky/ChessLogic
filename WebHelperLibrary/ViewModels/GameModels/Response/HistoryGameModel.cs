using System;
using System.Collections.Generic;
using System.Text;

namespace SharedWebObjectsLibrary.ViewModels.GameModels.Response
{
    public class HistoryGameModel
    {
        public int GameID { get; set; }
        public DateTime? EventTime { get; set; }
        public bool IsStartEvent { get; set; } = false;
        public string? WonBy { get; set; }
        public string PlayerWhite { get; set; }
        public string PlayerBlack { get; set; }
        public bool IsGameLive { get; set; }
    }
}
