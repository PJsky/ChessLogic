using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System.Collections.Generic;

namespace ChessLogicLibrary.ChessGameReplayers
{
    public interface IChessGameReplayer
    {
        List<IChessPiece> ReplayGame(List<Move> movesList, int? numberOfMoves = null);
        List<IChessPiece> ReplayGame(string movesString);
    }
}