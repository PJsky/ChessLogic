using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PreMoveConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.WinConditionsVerifiers
{
    public class CheckedKingCondition : IWinCondition
    {
        private IPreMoveCondition positionCheckedVerifier = new PositionChecked();
        public IGame Game { get; set; }
        public CheckedKingCondition(IGame game)
        {
            Game = game;
        }

        public ColorsEnum? Verify()
        {
            //Check if king will be checked after the current move
            List<IChessPiece> currentColorPieces = Game.chessBoard.ChessPiecesOnBoard.Where(cp => cp.Color == Game.chessTimer.ColorsTurn).ToList();
            List<string> currentColorPiecesPositions = currentColorPieces.Select(cp => cp.Position.ToString()).ToList();
            foreach(string positionString in currentColorPiecesPositions)
            {
                int boardHightBoundary = Game.chessBoard.UpperRightCorner.RowPosition;
                int boardWidthBoundary = Game.chessBoard.UpperRightCorner.ColumnPosition;
                for(int i = 1; i<= boardWidthBoundary; i++)
                    for(int j = 1; j<= boardHightBoundary; j++)
                    {
                        var movedPiece = Game.chessBoard.GetAPieceFromPosition(positionString);
                        bool isMovePossible = movedPiece.IsMovePossible(i,j,Game.chessBoard.ChessPiecesOnBoard);
                        if (!isMovePossible) continue;

                        Position finalPosition = new Position(i,j);
                        bool willKingBeChecked = positionCheckedVerifier.VerifyKingPosition(movedPiece, finalPosition.ToString(),
                                                                                            Game.chessBoard.ChessPiecesOnBoard, Game.chessTimer.ColorsTurn);
                        if (!willKingBeChecked && isMovePossible) return null;
                    }
            }

            ColorsEnum currentColor = Game.chessTimer.ColorsTurn;
            return currentColor == ColorsEnum.White ? ColorsEnum.Black : ColorsEnum.White; 
        }
    }
}
