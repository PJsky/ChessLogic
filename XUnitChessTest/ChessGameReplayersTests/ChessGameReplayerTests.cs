using ChessLogicLibrary;
using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessGameReplayers;
using ChessLogicLibrary.PlayerTurnObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessGameReplayersTests
{
    public class ChessGameReplayerTests
    {
        [Fact]
        public void GetReplayedGame_PassesProperMovesString_ReturnsPiecesWithProperNewPositions()
        {
            IGame game = new Game(new StandardChessBoard(new StandardChessPieceFactory()), new StandardChessTimer());
            IChessGameReplayer replayer = new ChessGameReplayer(game);
            string movesList = "A2:A4;A7:A5;A1:A3;B7:B5;A3:G3;G7:G5;G3:G5";
            replayer.ReplayGame(movesList);

            var cpList = game.chessBoard.ChessPiecesOnBoard;

            //Gives Pawns which were moved
            var PawnStartingA2 = cpList.SingleOrDefault(cp => cp.Position.ToString() == "A4");
            var PawnStartingA7 = cpList.SingleOrDefault(cp => cp.Position.ToString() == "A5");
            var PawnStartingB7 = cpList.SingleOrDefault(cp => cp.Position.ToString() == "B5");

            //Rook which moves and takes moved pawn
            var RookStartingA1 = cpList.SingleOrDefault(cp => cp.Position.ToString() == "G5");

            int AmountOfPawnsAtTheEnd = cpList.Where(cp => cp.Name == "Pawn").Count();

            //Check if the figures were taken from the positions correctly
            Assert.Equal("Pawn", PawnStartingA2.Name);
            Assert.Equal("Pawn", PawnStartingA7.Name);
            Assert.Equal("Pawn", PawnStartingB7.Name);

            Assert.Equal("Rook", RookStartingA1.Name);

            //Check if one pawn was taken
            Assert.Equal(15, AmountOfPawnsAtTheEnd);

        }
    }
}
