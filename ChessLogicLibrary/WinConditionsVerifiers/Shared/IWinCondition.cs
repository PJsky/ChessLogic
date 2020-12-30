using ChessLogicLibrary.ChessPieces;

namespace ChessLogicLibrary.WinConditionsVerifiers
{
    public interface IWinCondition
    {
        IGame Game { get; set; }
        ColorsEnum? Verify();
    }
}