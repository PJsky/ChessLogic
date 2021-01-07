using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.ChessGameReplayers
{
    public class ChessGameReplayer : IChessGameReplayer
    {
        private IGame game;
        public ChessGameReplayer(IGame Game)
        {
            game = Game;
        }

        public List<IChessPiece> ReplayGame(string movesString)
        {
            string[] stringMovesArray = movesString.Split(';');
            List<Move> movesList = stringMovesArray.Select(s => new Move(s)).ToList();
            return ReplayGame(movesList);
        }

        public List<IChessPiece> ReplayGame(List<Move> movesList)
        {
            foreach (Move move in movesList)
            {
                var start = move.StartingPosition.ToString();
                var finish = move.FinalPosition.ToString();
                bool result = game.MakeAMove(start, finish);
                if (!result) throw new ArgumentException("Passed invalid move list");
            }
            return game.chessBoard.ChessPiecesOnBoard;
        }
    }
}
