using ChessLogicLibrary.ChessPiecePosition;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public interface IChessPiece
    {
        string Name { get; }
        bool wasMoved { get; }
        ColorsEnum Color { get; set; }
        Position Position { get; }
        void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null);
    }
}
