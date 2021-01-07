using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessGameReplayer
{
    public class ChessGameReplayer
    {
        private IGame game;
        private List<IChessPiece> chessPieces = new List<IChessPiece>();
        public ChessGameReplayer(IGame Game)
        {
            game = Game;
            chessPieces = Game.chessBoard.ChessPiecesOnBoard;
        }

        public List<IChessPiece> GetReplayedGame(string MovesList)
        {
            throw new NotImplementedException();
        }
        public List<IChessPiece> GetReplayedGame(List<Position> MovesList)
        {
            throw new NotImplementedException();
        }
        public List<IChessPiece> GetReplayedGame(List<Move> MovesList)
        {
            throw new NotImplementedException();

        }
    }
}
