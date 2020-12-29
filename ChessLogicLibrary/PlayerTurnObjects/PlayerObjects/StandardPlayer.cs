using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.PlayerTurnObjects.PlayerObjects
{
    public class StandardPlayer : IPlayer
    {
        public string Name { get; }

        public StandardPlayer(string name)
        {
            Name = name;
        }
    }
}
