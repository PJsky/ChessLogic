using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessGameReplayer
{
    public class ChessGameReplayer
    {
        private List<IChessPiece> chessPieces = new List<IChessPiece>();
        public ChessGameReplayer(List<IChessPiece> ChessPieces)
        {
            chessPieces = ChessPieces;
        }

        public List<IChessPiece> GetReplayedGame(string MovesList)
        {
            throw new NotImplementedException();
        }
        public List<IChessPiece> GetReplayedGame(List<Position> MovesList)
        {
            throw new NotImplementedException();
        }
    }
}
