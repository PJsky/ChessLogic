using ChessLogicLibrary.ChessPieces;
using System.Collections.Generic;

namespace ChessLogicLibrary.ChessBoardObjects
{
    public interface IChessBoard
    {
        List<IChessPiece> chessPiecesOnBoard { get; }
        bool MoveAPiece(string startingPositionString, string finalPositionString);
    }
}