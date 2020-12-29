using ChessLogicLibrary;
using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.PlayerTurnObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.MainGameObjectTests
{
    public class GameTests
    {
        [Fact]
        public void MakeAMove_PassedVariousMoves_ReturnsTrueWhenMakingMoveOnYourTurn()
        {
            Game game = new Game(new StandardChessBoard(new StandardChessPieceFactory()),
                                new StandardChessTimer());
            bool firstSuccessfulMoveResult = game.MoveAPiece("A2", "A4");
            bool firstMoveOnOpponentsTurnResult = game.MoveAPiece("A4", "A5");
            bool secondSuccessfulMoveResult = game.MoveAPiece("A7", "A5");
            bool secondMoveOnOpponentsTurnResult = game.MoveAPiece("A5", "A4");
            game.MoveAPiece("B2", "B4");
            bool successfulAttackResult = game.MoveAPiece("A5", "B4");

            Assert.True(firstSuccessfulMoveResult);
            Assert.True(secondSuccessfulMoveResult);
            Assert.True(successfulAttackResult);

            Assert.False(firstMoveOnOpponentsTurnResult);
            Assert.False(secondMoveOnOpponentsTurnResult);

        }
    }
}
