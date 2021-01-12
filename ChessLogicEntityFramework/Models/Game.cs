using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicEntityFramework.Models
{
    public class Game
    {
        public int ID { get; set; }
        public int PlayerWhiteID { get; set; }
        public User PlayerWhite { get; set; }
        public int PlayerBlackID { get; set; }
        public User PlayerBlack { get; set; }
        public User Winner { get; set; }
        public string MovesList { get; set; }
    }
}
