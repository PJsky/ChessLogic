using ChessLogicEntityFramework.Models;
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
    public class StandardGameMapper : IGameMapper
    {
        private IUserDataAccess userDataAccess;
        public StandardGameMapper(IUserDataAccess UserDataAccess)
        {
            userDataAccess = UserDataAccess;
        }

        public ChessLogicLibrary.Game MapDbToGame(ChessLogicEntityFramework.Models.Game gameFromDb)
        {
            User playerWhite = null, playerBlack = null;
            if(gameFromDb.PlayerWhiteID == null)
                playerWhite = userDataAccess.GetUser((int)gameFromDb.PlayerWhiteID);
            if(gameFromDb.PlayerBlackID == null)
                playerBlack = userDataAccess.GetUser((int)gameFromDb.PlayerBlackID);

            var game = new ChessLogicLibrary.Game(new StandardChessBoard(new StandardChessPieceFactory()),
                                new StandardChessTimer(new StandardPlayer(playerWhite.Name), new StandardPlayer(playerBlack.Name)));

            game.winCondition = new CheckedKingCondition(game);
            ChessGameReplayer cgr = new ChessGameReplayer(game);
            //Replays all the moves from the database movesList
            cgr.ReplayGame(gameFromDb.MovesList);

            return game;
        }
    }
}
