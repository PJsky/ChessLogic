using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.EndGameResults;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using ChessLogicLibrary.WinConditionsVerifiers;

namespace ChessLogicLibrary
{
    public interface IGame
    {
        IChessBoard chessBoard { get; }
        IChessTimer chessTimer { get; }
        IWinCondition winCondition { get; set; }
        IEndGameResult endGameResult { get; set; }
        IPlayer winner { get; set; }
        //void EndGame();
        void HasGameFinished();
        bool MakeAMove(string startingPositionString, string finalPositionString);
        bool MoveAPiece(string startingPositionString, string finalPositionString);
        void StartGame();
    }
}