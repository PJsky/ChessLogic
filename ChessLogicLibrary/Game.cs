using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.EndGameResults;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using ChessLogicLibrary.WinConditionsVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary
{
    public class Game : IGame
    {
        public IChessBoard chessBoard { get; }
        public IChessTimer chessTimer { get; }
        public IWinCondition winCondition { get; set; }
        public IEndGameResult endGameResult { get; set; }
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

        public void MakeAMove(string startingPositionString, string finalPositionString)
        {
            MoveAPiece(startingPositionString, finalPositionString);
            HasGameFinished();
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

        // Checks for conditions within injected IWinCondition
        // Implement this interface if u wanna create new conditions to win the game
        public void HasGameFinished()
        {
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
