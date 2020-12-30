using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
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
        bool hasGameStarted = false;
        public Game(IChessBoard ChessBoard = null, IChessTimer ChessTimer = null, IWinCondition WinCondition = null)
        {
            chessBoard = ChessBoard;
            chessTimer = ChessTimer;
            winCondition = WinCondition;
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

        //Win condition verifier
        public void HasGameFinished()
        {
           if(winCondition.Verify() != null)
            EndGame();
        }

        //IEndGameResult
        public void EndGame()
        {
            //Do something when the game ends
        }
    }
}
