using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.WinConditionsVerifiers
{
    public class DeadKingCondition : IWinCondition
    {
        List<IChessPiece> chessPieces;
        public IGame Game { get; set; }
        public DeadKingCondition(List<IChessPiece> ChessPieces)
        {
            chessPieces = ChessPieces;
        }

        public DeadKingCondition(IGame game)
        {
            Game = game;
            chessPieces = Game.chessBoard.ChessPiecesOnBoard;
        }



        public ColorsEnum? Verify()
        {
            bool isWhiteKingAlive = chessPieces.Where(cp => cp.Name == "King" && cp.Color == ColorsEnum.White).Count() > 0;
            if (!isWhiteKingAlive) return ColorsEnum.Black;

            bool isBlackKingAlive = chessPieces.Where(cp => cp.Name == "King" && cp.Color == ColorsEnum.Black).Count() > 0;
            if (!isBlackKingAlive) return ColorsEnum.White;

            return null;
        }
    }
}
