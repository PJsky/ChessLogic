using ChessLogicLibrary;

namespace ChessSignalRLibrary.GameMapper
{
    public interface IGameMapper
    {
        Game MapDbToGame(ChessLogicEntityFramework.Models.Game game);
    }
}