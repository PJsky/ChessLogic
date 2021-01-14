using ChessLogicEntityFramework.OperationObjects;
using ChessLogicLibrary.ChessBoardSerializers;
using ChessLogicLibrary.ChessPiecePosition;
using ChessSignalRLibrary.GameMapper;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessSignalRLibrary.GameHubObjects
{
    public class GameHub : Hub
    {
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task MakeAMove(string moveString)
        {
            Move move = new Move(moveString);
            IGameDataAccess gameDataAccess = new GameDataAccess();
            IUserDataAccess userDataAccess = new UserDataAccess();
            IGameMapper gameMapper = new StandardGameMapper(userDataAccess);
            var gameFromDb = gameDataAccess.GetGame(1);
            var game = gameMapper.MapDbToGame(gameFromDb);
            bool wasMoveMade = game.MakeAMove(move.StartingPosition.ToString(),move.FinalPosition.ToString());
            if (wasMoveMade)
            {
                gameDataAccess.AddMovesToList(1, moveString + ";");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.All.SendAsync("ReceiveBoard", serializedBoard);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "wrong move");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.All.SendAsync("ReceiveBoard", serializedBoard);
            }
        }
    }
}
