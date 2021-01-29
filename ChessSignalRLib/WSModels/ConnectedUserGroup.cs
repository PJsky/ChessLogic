using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSignalRLibrary.WSModels
{
    public class ConnectedUserGroup
    {
        public int UserID { get; set; }
        public string ConnectionID { get; set; }
        public int GameRoomID { get; set; }
    }
}
