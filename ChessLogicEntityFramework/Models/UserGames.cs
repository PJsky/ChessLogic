using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicEntityFramework.Models
{
    public class UserGames
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int GameID { get; set; }
        public Game Game { get; set; }
    }
}
