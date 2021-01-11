using ChessLogicEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebApiWithSockets.ViewModels.GameModels
{
    public class NewGameModel
    {
        public User PlayerWhite { get; set; }
    }
}
