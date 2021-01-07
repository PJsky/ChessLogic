using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPiecePosition
{
    public class Move
    {
        public Move(){ }
        public Move(string startingPositionString, string finalPositionString)
        {
            StartingPosition = new Position(startingPositionString);
            FinalPosition = new Position(finalPositionString);
        }

        public Move(Position startingPosition, Position finalPosition)
        {
            StartingPosition = startingPosition;
            FinalPosition = finalPosition;
        }

        public Position StartingPosition { get; set; }
        public Position FinalPosition { get; set; }

    }
}
