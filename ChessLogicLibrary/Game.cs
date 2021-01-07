using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.EndGameResults;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using ChessLogicLibrary.PreMoveConditions;
using ChessLogicLibrary.WinConditionsVerifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary
{
    public class Game : IGame
    {
        public IChessBoard chessBoard { get; }
        public IChessTimer chessTimer { get; }
        public IWinCondition winCondition { get; set; }
        public IEndGameResult endGameResult { get; set; }
        private IPreMoveCondition positionCheckedVerifier = new PositionChecked();
        public IPlayer winner { get; set; }
        bool hasGameStarted = false;
        public Game(IChessBoard ChessBoard = null, IChessTimer ChessTimer = null
                    ,IWinCondition WinCondition = null, IEndGameResult EndGameResult = null)
        {
            chessBoard = ChessBoard;
            chessTimer = ChessTimer;
            winCondition = WinCondition;
            endGameResult = EndGameResult;
        }

        public bool MakeAMove(string startingPositionString, string finalPositionString)
        {
            //IMoveCondition.Verify == present a condition that has to be met before moving any piece
            //ex. Can't move if king is checked ///// Can't move if the move will make your own king checked
            IChessPiece currentKing = chessBoard.ChessPiecesOnBoard.Where(cp => cp.Name == "King" && cp.Color == chessTimer.ColorsTurn).FirstOrDefault(); 
            
            //bool isKingChecked = positionCheckedVerifier.Verify(currentKing.Position, chessBoard.ChessPiecesOnBoard, currentKing.Color);
            //Check if king will be checked after the current move
            bool willKingBeChecked = positionCheckedVerifier.VerifyKingPosition(chessBoard.GetAPieceFromPosition(startingPositionString), 
                                                                                finalPositionString, chessBoard.ChessPiecesOnBoard, chessTimer.ColorsTurn);
            bool hasBeenMoved = false;
            if(!willKingBeChecked)
                hasBeenMoved = MoveAPiece(startingPositionString, finalPositionString);

            HasGameFinished();
            return hasBeenMoved;
        }

        public bool MoveAPiece(string startingPositionString, string finalPositionString)
        {
            IChessPiece movedPiece = chessBoard.GetAPieceFromPosition(startingPositionString);
            if (movedPiece == null) return false;
            if (chessBoard.GetAPieceFromPosition(startingPositionString).Color == chessTimer.ColorsTurn)
            {
                bool hasAPieceBeenMoved = chessBoard.MoveAPiece(startingPositionString, finalPositionString);
                if (hasAPieceBeenMoved)
                {
                    if (!hasGameStarted)
                        StartGame();

                    chessTimer.ChangeTurn();
                }
                return hasAPieceBeenMoved;
            }
            return false;
        }

        public void StartGame()
        {
            chessTimer.StartTimer();
            hasGameStarted = true;
        }

        // Checks for conditions within injected IWinCondition
        // Implement this interface if u wanna create new conditions to win the game
        public void HasGameFinished()
        {
            if (winCondition == null) return;
            var winnerColor = winCondition.Verify();
            if (winnerColor == ColorsEnum.White) winner = chessTimer.PlayerWhite;
            else if (winnerColor == ColorsEnum.Black) winner = chessTimer.PlayerBlack;
            if(winner != null)
                EndGame();
        }

        // Executes a function inside injected endGameResult
        // Create a class implementing IEndGameResult to do certain logic after the game has finished
        private void EndGame()
        {
            if(endGameResult != null)
                endGameResult.FinishGame(winner);
        }
    }
}
