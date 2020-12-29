using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.PlayerTurnObjects
{
    public class StandardChessTimer : IChessTimer
    {
        public IPlayer PlayerWhite { get; }
        public IPlayer PlayerBlack { get; }
        public ColorsEnum ColorsTurn { get; private set; } = ColorsEnum.White;
        public StandardChessTimer(IPlayer playerWhite, IPlayer playerBlack)
        {
            PlayerWhite = playerWhite;
            PlayerBlack = playerBlack;
        }

        public void ChangeTurn()
        {
            if (ColorsTurn == ColorsEnum.White)
                ColorsTurn = ColorsEnum.Black;
            else
                ColorsTurn = ColorsEnum.White;
        }

        public void StartTimer()
        {
            Console.WriteLine("Timer Started");
        }
    }
}
