using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.EndGameResults
{
    public interface IEndGameResult
    {
        void FinishGame(IPlayer winner);
    }
}
