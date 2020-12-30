using ChessLogicLibrary.ChessPieces;

namespace ChessLogicLibrary.WinConditionsVerifiers
{
    public interface IWinCondition
    {
        ColorsEnum? Verify();
    }
}