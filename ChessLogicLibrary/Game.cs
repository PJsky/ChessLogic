using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.EndGameResults;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.WinConditionsVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary
{
    public class Game : IGame
    {
        IChessBoard chessBoard;
        IChessTimer chessTimer;
        IWinCondition winCondition;
        IEndGameResult endGameResult;
        bool hasGameStarted = false;
        public Game(IChessBoard ChessBoard = null, IChessTimer ChessTimer = null, 
                    IWinCondition WinCondition = null, IEndGameResult EndGameResult = null)
        {
            chessBoard = ChessBoard;
            chessTimer = ChessTimer;
            winCondition = WinCondition;
            endGameResult = EndGameResult;
        }

        public bool MoveAPiece(string startingPositionString, string finalPositionString)
        {
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

        /// <summary>
        /// Checks for conditions within injected IWinCondition
        /// Implement this interface if u wanna create new conditions to win the game
        /// </summary>
        public void HasGameFinished()
        {
           if(winCondition.Verify() != null)
            EndGame();
        }

        /// <summary>
        /// Executes a function inside injected endGameResult
        /// Create a class implementing IEndGameResult to do certain logic after the game has finished
        /// </summary>
        public void EndGame()
        {
            if(endGameResult != null)
                endGameResult.FinishGame();
        }
    }
}
