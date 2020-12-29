using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PlayerTurnObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary
{
    public class Game
    {
        IChessBoard chessBoard;
        IChessTimer chessTimer;
        bool hasGameStarted = false;
        public Game(IChessBoard ChessBoard, IChessTimer ChessTimer)
        {
            chessBoard = ChessBoard;
            chessTimer = ChessTimer;
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

    }
}
