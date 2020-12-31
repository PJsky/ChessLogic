using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PreMoveConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories;

namespace XUnitChessTest.PreMoveConditionsTests
{
    public class PositionCheckedTests
    {
        PositionChecked positionChecked = new PositionChecked();
        List<IChessPiece> cpList = new TestingChessPieceFactory().GetChessPieces();
        List<IChessPiece> cpListWithKing = new KingCheckedTestPieceFactory().GetChessPieces();


        [Theory]
        [InlineData("B5")]
        [InlineData("B1")]
        [InlineData("E2")]
        [InlineData("C2")]
        [InlineData("D2")]
        public void VerifyPosition_PassCheckedPositionByBlack_ReturnsTrue(string position)
        {
            bool result = positionChecked.VerifyPosition(position, cpList, ColorsEnum.White);

            Assert.True(result);
        }

        [Theory]
        [InlineData("C3")]
        [InlineData("A2")]
        [InlineData("D2")]
        public void VerifyPosition_PassCheckedPositionByWhite_ReturnsTrue(string position)
        {
            bool result = positionChecked.VerifyPosition(position, cpList, ColorsEnum.Black);

            Assert.True(result);
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("C3")]
        [InlineData("A2")]
        [InlineData("Z4")]
        public void VerifyPosition_PassUncheckedPositionByBlack_ReturnsFalse(string position)
        {
            bool result = positionChecked.VerifyPosition(position, cpList, ColorsEnum.White);

            Assert.False(result);
        }

        [Theory]
        [InlineData("B5")]
        [InlineData("E2")]
        [InlineData("C2")]
        [InlineData("Z4")]
        public void VerifyPosition_PassUncheckedPositionByWhite_ReturnsFalse(string position)
        {
            bool result = positionChecked.VerifyPosition(position, cpList, ColorsEnum.Black);

            Assert.False(result);
        }

        [Theory]
        [InlineData("B5", "A4")]
        [InlineData("C5", "C4")]
        [InlineData("D4", "D2")]
        public void VerifyKingPosition_PassCheckedPosition_ReturnsTrue(string startPositionString, string finalPositionString)
        {
            Position startPosition = new Position(startPositionString);
            IChessPiece pieceMoved = cpListWithKing.Where(cp => cp.Position.ColumnPosition == startPosition.ColumnPosition && cp.Position.RowPosition == startPosition.RowPosition).FirstOrDefault();
            bool result = positionChecked.VerifyKingPosition(pieceMoved, finalPositionString, cpListWithKing, pieceMoved.Color);

            Assert.True(result);
        }

        [Theory]
        [InlineData("B5", "C4")]
        [InlineData("D5", "C4")]
        [InlineData("C5", "D6")]
        public void VerifyKingPosition_PassUncheckedPosition_ReturnsFalse(string startPositionString, string finalPositionString)
        {
            Position startPosition = new Position(startPositionString);
            IChessPiece pieceMoved = cpListWithKing.Where(cp => cp.Position.ColumnPosition == startPosition.ColumnPosition && cp.Position.RowPosition == startPosition.RowPosition).FirstOrDefault();
            bool result = positionChecked.VerifyKingPosition(pieceMoved, finalPositionString, cpListWithKing, pieceMoved.Color);

            Assert.False(result);
        }
    }
}
