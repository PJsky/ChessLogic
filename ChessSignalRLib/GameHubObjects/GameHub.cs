using ChessLogicEntityFramework.OperationObjects;
using ChessLogicLibrary.ChessBoardSerializers;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessSignalRLibrary.GameMapper;
using ChessSignalRLibrary.GameUserDictonary;
using ChessSignalRLibrary.WSModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSignalRLibrary.GameHubObjects
{
    public class GameHub : Hub
    {
        //Dictionary<string, string> dict = GameHubDictionary.Get();
        List<ConnectedUserGroup> connectionList = GameHubDictionary.connectionList;

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
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveMessage", "the winner is: " + gameFromDb.Winner.ID);
                return; 
            }

            var game = gameMapper.MapDbToGame(gameFromDb);



            bool wasMoveMade = game.MakeAMove(move.StartingPosition.ToString(), move.FinalPosition.ToString());
            if (wasMoveMade)
            {
                gameDataAccess.AddMovesToList(gameRoomID, moveString + ";");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.Group("gameRoom_"+gameFromDb.ID).SendAsync("ReceiveBoard", serializedBoard);
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveTurn",
                new { lastTurnMove = gameFromDb.MovesList.Substring(gameFromDb.MovesList.Length - 6) });
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "wrong move");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveBoard", serializedBoard);
            }

            if (game.winner != null) { 
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveWinner", game.winner.Name);
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
            if (game.PlayerWhiteID == null && game.PlayerBlackID != Int32.Parse(userID))
            {
                UserDataAccess userDataAccess = new UserDataAccess();
                var user = userDataAccess.GetUser(Int32.Parse(userID));
                gameDataAccess.ChangePlayers(game.ID, user, game.PlayerBlack);
            }
            else if (game.PlayerBlackID == null && game.PlayerWhiteID != Int32.Parse(userID))
            {
                UserDataAccess userDataAccess = new UserDataAccess();
                var user = userDataAccess.GetUser(Int32.Parse(userID));
                gameDataAccess.ChangePlayers(game.ID, game.PlayerWhite, user);
            }
            if (game.PlayerBlackID == Int32.Parse(userID) || game.PlayerWhiteID == Int32.Parse(userID)) {

                Groups.AddToGroupAsync(Context.ConnectionId, "gameRoom_" + gameRoomID);

                //dict.Add(Context.ConnectionId, "gameRoom_" + gameRoomID);
                connectionList.RemoveAll(c => c.ConnectionID == Context.ConnectionId || c.UserID == Int32.Parse(userID));
                connectionList.Add(new ConnectedUserGroup
                {
                    ConnectionID = Context.ConnectionId,
                    UserID = Int32.Parse(userID),
                    GameRoomID = Int32.Parse(gameRoomID)
                });

                var gameMapper = new StandardGameMapper(new UserDataAccess());
                var gameObject = gameMapper.MapDbToGame(game);
                var serializedBoard = StandardChessBoardSerializer.Serialize(gameObject.chessBoard);
                Clients.Group("gameRoom_" + gameRoomID).SendAsync("ReceivePlayers", new { p1 = gameObject.chessTimer.PlayerWhite.Name, p2 = gameObject.chessTimer.PlayerBlack.Name });
                if(game.MovesList != null && game.MovesList.Length >= 6)
                Clients.Caller.SendAsync("ReceiveTurn", 
                    new { lastTurnMove = game.MovesList.Substring(game.MovesList.Length-6)});
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
            if((gameObject.chessTimer.ColorsTurn == ColorsEnum.White && game.PlayerWhiteID == Int32.Parse(userID)) ||
               (gameObject.chessTimer.ColorsTurn == ColorsEnum.Black && game.PlayerBlackID == Int32.Parse(userID)))
            return MakeAMove(Int32.Parse(gameRoomID), moveString);

            return Clients.Caller.SendAsync("ReceiveMessage", "Tried to move on other players turn");
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.SendAsync("ReceiveMessage", "disconnected: " + Context.ConnectionId);
            QuitGroups();

            Console.WriteLine(connectionList);
            return base.OnDisconnectedAsync(exception);
        }


        public Task QuitGroups()
        {
            GameDataAccess gameDataAccess = new GameDataAccess();
            var connection = connectionList.Where(con => con.ConnectionID == Context.ConnectionId).FirstOrDefault();
            if (connection == null) return Clients.Caller.SendAsync("ReceiveMessage", "Not conneted to any");
            var gameFromDb = gameDataAccess.GetGame(connection.GameRoomID);
            connectionList.RemoveAll(con => con.ConnectionID == Context.ConnectionId);

            if (gameFromDb.PlayerWhiteID == connection.UserID)
                gameDataAccess.ChangePlayers(gameFromDb.ID, null, gameFromDb.PlayerBlack);

            else if (gameFromDb.PlayerBlackID == connection.UserID)
                gameDataAccess.ChangePlayers(gameFromDb.ID, gameFromDb.PlayerWhite, null);

            //if (gameFromDb.PlayerBlackID == null && gameFromDb.PlayerWhiteID == null)
            //    gameDataAccess.RemoveGame(gameFromDb);

            if (gameFromDb.PlayerBlackID == null && gameFromDb.PlayerWhiteID == null)
                gameDataAccess.FinishGame(gameFromDb.ID);


            Clients.Group("gameRoom_" + connection.GameRoomID).SendAsync("ReceivePlayers", 
                new { 
                    p1 = gameFromDb.PlayerWhite!=null? gameFromDb.PlayerWhite.Name : null, 
                    p2 = gameFromDb.PlayerBlack != null ? gameFromDb.PlayerBlack.Name : null
                });
            return Clients.Caller.SendAsync("ReceiveMessage", "You were remvoed from all groups");
        }
    }
}
