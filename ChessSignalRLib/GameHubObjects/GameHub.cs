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

        private async Task MakeAMove(int gameRoomID, string moveString)
        {
            Move move = new Move(moveString);
            IGameDataAccess gameDataAccess = new GameDataAccess();
            IUserDataAccess userDataAccess = new UserDataAccess();
            IGameMapper gameMapper = new StandardGameMapper(userDataAccess);
            var gameFromDb = gameDataAccess.GetGame(gameRoomID);

            if (gameFromDb.Winner != null) {
                await Clients.All.SendAsync("ReceiveMessage", "the winner is: " + gameFromDb.Winner.ID);
                return; 
            }

            var game = gameMapper.MapDbToGame(gameFromDb);



            bool wasMoveMade = game.MakeAMove(move.StartingPosition.ToString(), move.FinalPosition.ToString());
            if (wasMoveMade)
            {
                gameDataAccess.AddMovesToList(gameRoomID, moveString + ";");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.All.SendAsync("ReceiveBoard", serializedBoard);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "wrong move");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.All.SendAsync("ReceiveBoard", serializedBoard);
            }

            if (game.winner != null) { 
                await Clients.All.SendAsync("ReceiveMessage", "the winner is: " + game.winner.Name);
                gameDataAccess.DecideWinner(gameFromDb.ID,userDataAccess.GetUser(game.winner.Name));
            }
        }

        public Task JoinGameGroup(string gameRoomID)
        {
            //Check if person is inside the game
            //Need to create additional table for users in games (anonymous)
            GameDataAccess gameDataAccess = new GameDataAccess();
            var game = gameDataAccess.GetGame(Int32.Parse(gameRoomID));
            if (game == null) return Clients.Caller.SendAsync("ReceiveMessage", "Tried to join a game which does not exist");

            var userID = Context.User.Identity.Name;
            if (userID == null) return Clients.Caller.SendAsync("You are not logged in");
            if (game.PlayerBlackID == Int32.Parse(userID) || game.PlayerWhiteID == Int32.Parse(userID)) {

                Groups.AddToGroupAsync(Context.ConnectionId, "gameRoom_" + gameRoomID);
                var gameMapper = new StandardGameMapper(new UserDataAccess());
                var gameObject = gameMapper.MapDbToGame(game);
                var serializedBoard = StandardChessBoardSerializer.Serialize(gameObject.chessBoard);
                return Clients.Caller.SendAsync("ReceiveBoard", serializedBoard);
            }

            return Clients.Caller.SendAsync("ReceiveMessage", "Tried to join full gameroom");
        }

        public Task SendMessageToGroup(string gameRoomID, string moveString)
        {
            //Need to make one player black/white and recognize it as such
            GameDataAccess gameDataAccess = new GameDataAccess();
            var game = gameDataAccess.GetGame(Int32.Parse(gameRoomID));
            if (game == null) return Clients.Caller.SendAsync("ReceiveMessage", "Tried to Send message to group which does not exist");

            var userID = Context.User.Identity.Name;
            if (game.PlayerBlackID != Int32.Parse(userID) && game.PlayerWhiteID != Int32.Parse(userID))
                return Clients.Caller.SendAsync("ReceiveMessage", "Tried to send message to a room which you do not belong to");

            var gameMapper = new StandardGameMapper(new UserDataAccess());
            var gameObject = gameMapper.MapDbToGame(game);
            if((gameObject.chessTimer.ColorsTurn == ChessLogicLibrary.ChessPieces.ColorsEnum.White && game.PlayerWhiteID == Int32.Parse(userID)) ||
               (gameObject.chessTimer.ColorsTurn == ChessLogicLibrary.ChessPieces.ColorsEnum.Black && game.PlayerBlackID == Int32.Parse(userID)))
            return MakeAMove(Int32.Parse(gameRoomID), moveString);

            return Clients.Caller.SendAsync("ReceiveMessage", "Tried to move on other players turn");
        }

    }
}
