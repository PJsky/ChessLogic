using ChessLogicLibrary.ChessPiecePosition;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public abstract class StandardChessPiece : IChessPiece
    {
        public abstract string Name { get; }
        public bool wasMoved { get; protected set; } = false;
        public StandardChessPiece(int colorId, int columnPosition, int rowPosition)
        {
            Color = (ColorsEnum)colorId;
            Position = new Position(columnPosition, rowPosition);
        }
        public ColorsEnum Color { get; set; }
        public Position Position { get; protected set; }
        public abstract void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null);

    }
}
