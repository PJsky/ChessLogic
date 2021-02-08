using ChessLogicEntityFramework.Models;
using ChessLogicEntityFramework.OperationObjects;
using ChessLogicLibrary.ChessBoardSerializers;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessSignalRLibrary.GameMapper;
using ChessSignalRLibrary.GameTimer;
using ChessSignalRLibrary.GameUserDictonary;
using ChessSignalRLibrary.WSModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ChessSignalRLibrary.GameHubObjects
{
    public class GameHub : Hub
    {
        public static IHubContext<GameHub> GlobalContext { get; private set; }
        public GameHub(IHubContext<GameHub> ctx)
        {
            GlobalContext = ctx;
        }
        //Dictionary<string, string> dict = GameHubDictionary.Get();
        List<ConnectedUserGroup> connectionList = GameHubDictionary.connectionList;
        Dictionary<int, GameTimers> gameTimers = GameTimersDictionary.gameTimersDictionary;
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
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveBoard", serializedBoard);
                
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
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveWinner", new { winner = gameFromDb.Winner.Name });
                return; 
            }

            if (gameFromDb.PlayerBlack == null || gameFromDb.PlayerWhite == null) return;

            var game = gameMapper.MapDbToGame(gameFromDb);



            bool wasMoveMade = game.MakeAMove(move.StartingPosition.ToString(), move.FinalPosition.ToString());
            if (wasMoveMade)
            {
                gameDataAccess.AddMovesToList(gameRoomID, moveString + ";");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.Group("gameRoom_"+gameFromDb.ID).SendAsync("ReceiveBoard", serializedBoard);
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveTurn",
                new { lastTurnMove = gameFromDb.MovesList.Substring(gameFromDb.MovesList.Length - 6) });

                //Start game on first move
                if (!gameTimers.ContainsKey(gameRoomID))
                {
                    //gameTimers.Add(Int32.Parse(gameRoomID), new GameTimers());
                    GameTimersDictionary.Add(gameRoomID, gameFromDb.PlayerWhite.Name, gameFromDb.PlayerBlack.Name, gameFromDb.GameTime, gameFromDb.TimeGain);
                    gameTimers[gameRoomID].StartGame();
                    await Clients.Group("FriendsWith_" + gameFromDb.PlayerWhiteID).SendAsync("ReceiveMessage", new
                    {
                        message = "fuck you!! from white"
                    });
                    await Clients.Group("FriendsWith_" + gameFromDb.PlayerBlackID).SendAsync("ReceiveMessage", new
                    {
                        message = "fuck you!! from black"
                    });
                }
                gameTimers[gameRoomID].ChangeTurn();
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveTime", new
                {
                    whiteTime = gameTimers[gameFromDb.ID].WhiteTime,
                    blackTime = gameTimers[gameFromDb.ID].BlackTime,
                    ticking = game.chessTimer.ColorsTurn.ToString()
                });
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "wrong move");
                var serializedBoard = StandardChessBoardSerializer.Serialize(game.chessBoard);
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveBoard", serializedBoard);
            }

            if (game.winner != null) {
                await Clients.Group("gameRoom_" + gameFromDb.ID).SendAsync("ReceiveWinner", new { winner = game.winner.Name });

                var winnerID = gameFromDb.PlayerWhite.Name == game.winner.Name ? gameFromDb.PlayerWhiteID : null;
                winnerID = gameFromDb.PlayerBlack.Name == game.winner.Name ? gameFromDb.PlayerBlackID : null;
                gameDataAccess.DecideWinner(gameFromDb.ID, (int)winnerID);
                gameDataAccess.FinishGame(gameFromDb.ID);
                GameTimersDictionary.gameTimersDictionary[gameFromDb.ID].CloseGame();
                gameTimers.Remove(gameFromDb.ID);
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

                connectionList.RemoveAll(c => c.ConnectionID == Context.ConnectionId || c.UserID == Int32.Parse(userID));
                connectionList.Add(new ConnectedUserGroup
                {
                    ConnectionID = Context.ConnectionId,
                    UserID = Int32.Parse(userID),
                    GameRoomID = Int32.Parse(gameRoomID)
                });


            }

                Groups.AddToGroupAsync(Context.ConnectionId, "gameRoom_" + gameRoomID);
                var gameMapper = new StandardGameMapper(new UserDataAccess());
                var gameObject = gameMapper.MapDbToGame(game);
                var serializedBoard = StandardChessBoardSerializer.Serialize(gameObject.chessBoard);
                Clients.Group("gameRoom_" + gameRoomID).SendAsync("ReceivePlayers", new { p1 = gameObject.chessTimer.PlayerWhite.Name, p2 = gameObject.chessTimer.PlayerBlack.Name });
                if(game.MovesList != null && game.MovesList.Length >= 6)
                Clients.Caller.SendAsync("ReceiveTurn", 
                    new { lastTurnMove = game.MovesList.Substring(game.MovesList.Length-6)});
                
                if(gameTimers.ContainsKey(game.ID))
                    Clients.Group("gameRoom_" + game.ID).SendAsync("ReceiveTime", new
                    {
                        ticking = gameObject.chessTimer.ColorsTurn.ToString(),
                        whiteTime = gameTimers[game.ID].WhiteTime,
                        blackTime = gameTimers[game.ID].BlackTime
                    });
                else
                    Clients.Group("gameRoom_" + game.ID).SendAsync("ReceiveTime", new
                    {
                        ticking = game.MovesList != null && game.MovesList.Length>5?gameObject.chessTimer.ColorsTurn.ToString():null,
                        whiteTime = game.GameTime,
                        blackTime = game.GameTime
                    });
            


                return Clients.Caller.SendAsync("ReceiveBoard", serializedBoard);

            //Add spectators
            //Groups.AddToGroupAsync(Context.ConnectionId, "gameRoom_" + gameRoomID);

            //return Clients.Caller.SendAsync("ReceiveMessage", "Tried to join full gameroom");
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
            var userDataAccess = new UserDataAccess();
            var userID = Context.User.Identity.Name;
            List<int> friendsIDs = new List<int>();
            if(userID != null)
                friendsIDs = userDataAccess.GetAllFriends(Int32.Parse(userID))
                                           .Select(f => f.User1ID == Int32.Parse(userID)? f.User2ID : f.User1ID)
                                           .ToList();

            for(int i = 0; i < friendsIDs.Count; i++)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "FriendsWith_" + friendsIDs[i]);
            }
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

            if (gameFromDb.PlayerWhiteID == connection.UserID && !gameTimers.ContainsKey(gameFromDb.ID))
                gameDataAccess.ChangePlayers(gameFromDb.ID, null, gameFromDb.PlayerBlack);

            else if (gameFromDb.PlayerBlackID == connection.UserID && !gameTimers.ContainsKey(gameFromDb.ID))
                gameDataAccess.ChangePlayers(gameFromDb.ID, gameFromDb.PlayerWhite, null);

            //if (gameFromDb.PlayerWhiteID == connection.UserID || gameTimers.ContainsKey(gameFromDb.ID))
            //    gameDataAccess.ChangePlayers(gameFromDb.ID, null, gameFromDb.PlayerBlack);

            //else if (gameFromDb.PlayerBlackID == connection.UserID || gameTimers.ContainsKey(gameFromDb.ID))
            //    gameDataAccess.ChangePlayers(gameFromDb.ID, gameFromDb.PlayerWhite, null);

            //if (gameFromDb.PlayerBlackID == null && gameFromDb.PlayerWhiteID == null)
            //    gameDataAccess.RemoveGame(gameFromDb);

            //if (gameFromDb.PlayerBlackID == null && gameFromDb.PlayerWhiteID == null)
            //    gameDataAccess.FinishGame(gameFromDb.ID);


            Clients.Group("gameRoom_" + connection.GameRoomID).SendAsync("ReceivePlayers", 
                new { 
                    p1 = gameFromDb.PlayerWhite!=null? gameFromDb.PlayerWhite.Name : null, 
                    p2 = gameFromDb.PlayerBlack != null ? gameFromDb.PlayerBlack.Name : null
                });
            return Clients.Caller.SendAsync("ReceiveMessage", "You were remvoed from all groups");
        }

     

    }
}
