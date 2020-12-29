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
            
        }

        //ChessBoard cb = new ChessBoard();
        //cb.Move("2B","4B");

        public void StartGame()
        {
            chessTimer.StartTimer();
            hasGameStarted = true;
        }

        //public void Move()
        //{
        //    if (MakeAMove("2B", "4B"))
        //    {
        //        Timer.ChangePlayer();
        //        if (!hasGameStarted)
        //            StartGame();
        //    }

        //}
    }
}
