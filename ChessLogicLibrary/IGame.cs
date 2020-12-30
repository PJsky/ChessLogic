using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.PlayerTurnObjects;

namespace ChessLogicLibrary
{
    public interface IGame
    {
        void EndGame();
        void HasGameFinished();
        bool MoveAPiece(string startingPositionString, string finalPositionString);
        void StartGame();
    }
}