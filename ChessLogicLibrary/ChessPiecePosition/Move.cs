using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPiecePosition
{
    public class Move
    {
        public Move() { }
        /// <summary>
        /// Converts moveString(ex. "A2:A4") into a move
        /// </summary>
        /// <param name="moveString"></param>
        public Move(string moveString)
        {
            string[] positions = moveString.Split(':');
            StartingPosition = new Position(positions[0]);
            FinalPosition = new Position(positions[1]);
        }
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
