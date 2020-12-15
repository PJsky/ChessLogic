using System;
using System.Collections.Generic;
using System.Text;
using ChessLogicLibrary.ChessPiecePosition;

namespace ChessLogicLibrary.ChessPieces
{
    public class Bishop : IChessPiece
    {
        public Bishop(int colorId, int columnPosition, int rowPosition)
        {
            Color = (ColorsEnum)colorId;
            position = new Position(columnPosition, rowPosition);
        }
        public string Name { get; set; }
        public ColorsEnum Color { get; set; }
        private Position position;
        public Position Position { get { return position; } }
    }
}
