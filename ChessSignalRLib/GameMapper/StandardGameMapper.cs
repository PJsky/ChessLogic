using ChessLogicEntityFramework.OperationObjects;
using ChessLogicLibrary;
using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessGameReplayers;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using ChessLogicLibrary.WinConditionsVerifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessSignalRLibrary.GameMapper
{
    public class StandardGameMapper : IStandardGameMapper
    {
        private IUserDataAccess userDataAccess;
        public StandardGameMapper(IUserDataAccess UserDataAccess)
        {
            userDataAccess = UserDataAccess;
        }

        public Game MapDbToGame(ChessLogicEntityFramework.Models.Game gameFromDb)
        {
            var playerWhite = userDataAccess.GetUser(gameFromDb.PlayerWhiteID);
            var playerBlack = userDataAccess.GetUser(gameFromDb.PlayerBlackID);

            var game = new Game(new StandardChessBoard(new StandardChessPieceFactory()),
                                new StandardChessTimer(new StandardPlayer(playerWhite.Name), new StandardPlayer(playerBlack.Name)));

            game.winCondition = new CheckedKingCondition(game);
            ChessGameReplayer cgr = new ChessGameReplayer(game);
            //Replays all the moves from the database movesList
            cgr.ReplayGame(gameFromDb.MovesList);

            return game;
        }
    }
}
