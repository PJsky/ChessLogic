using ChessLogicLibrary;

namespace ChessSignalRLibrary.GameMapper
{
    public interface IStandardGameMapper
    {
        Game MapDbToGame(ChessLogicEntityFramework.Models.Game game);
    }
}