using ChessLogicLibrary.ChessPieces;
using System.Collections.Generic;

namespace ChessLogicLibrary.ChessBoardObjects
{
    public interface IChessPieceFactory
    {
        List<IChessPiece> GetChessPieces();
    }
}