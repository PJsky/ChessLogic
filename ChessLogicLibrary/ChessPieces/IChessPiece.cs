using ChessLogicLibrary.ChessPiecePosition;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public interface IChessPiece
    {
        ColorsEnum Color { get; set; }
        string Name { get; set; }
        Position Position { get;  }
    }
}
