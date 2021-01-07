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

        public List<IChessPiece> ReplayGame(List<Move> movesList, int? numberOfMoves = null)
        {
            //If number of moves is not specified make all the moves
            numberOfMoves = numberOfMoves ?? movesList.Count;

            for(int i =0; i< numberOfMoves; i++)
            {
                var start = movesList[i].StartingPosition.ToString();
                var finish = movesList[i].FinalPosition.ToString();
                bool result = game.MakeAMove(start, finish);
                if (!result) throw new ArgumentException("Passed invalid move list");
            }
            return game.chessBoard.ChessPiecesOnBoard;
        }
    }
}
