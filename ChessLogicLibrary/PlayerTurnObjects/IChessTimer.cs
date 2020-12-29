using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;

namespace ChessLogicLibrary.PlayerTurnObjects
{
    public interface IChessTimer
    {
        IPlayer PlayerWhite { get; }
        IPlayer PlayerBlack { get; }
        ColorsEnum ColorsTurn { get; }

        void ChangeTurn();
        void StartTimer();
    }
}