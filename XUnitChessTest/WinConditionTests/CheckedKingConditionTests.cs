using ChessLogicLibrary;
using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.WinConditionsVerifiers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories;

namespace XUnitChessTest.WinConditionTests
{
    public class CheckedKingConditionTests
    {
        private IChessPieceFactory cpFactory;
        private IChessBoard chessBoard;
        private IGame game;
        private IWinCondition checkedKingCondition;
        private void Setup(int setupNumber)
        {
            cpFactory = new KingCheckMatedTestPieceFactory(setupNumber);
            chessBoard = new StandardChessBoard(cpFactory);
            game = new Game(chessBoard, new StandardChessTimer());
            checkedKingCondition = new CheckedKingCondition(game);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Verify_KingCheckmated_ReturnsOpposingColor(int setupNumber)
        {
            Setup(setupNumber);

            var result = checkedKingCondition.Verify();

            Assert.Equal(ColorsEnum.Black, result);
        }

        [Fact]
        public void Verify_KingCanEvade_ReturnsNull()
        {
            Setup(3);

            var result = checkedKingCondition.Verify();

            Assert.Null(result);
        }

        [Fact]
        public void Verify_CanAttackCheckmate_ReturnsNull()
        {
            Setup(4);

            var result = checkedKingCondition.Verify();

            Assert.Null(result);
        }
    }
}
