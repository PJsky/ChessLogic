using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PreMoveConditions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories;

namespace XUnitChessTest.PreMoveConditionsTests
{
    public class PositionCheckedTests
    {
        PositionChecked positionChecked = new PositionChecked();
        List<IChessPiece> cpList = new TestingChessPieceFactory().GetChessPieces();


        [Theory]
        [InlineData("B5")]
        [InlineData("B1")]
        [InlineData("E2")]
        [InlineData("C2")]
        [InlineData("D2")]
        public void Verify_PassCheckedPositionByBlack_ReturnsTrue(string position)
        {
            bool result = positionChecked.Verify(position, cpList, ColorsEnum.White);

            Assert.True(result);
        }

        [Theory]
        [InlineData("C3")]
        [InlineData("A2")]
        [InlineData("D2")]
        public void Verify_PassCheckedPositionByWhite_ReturnsTrue(string position)
        {
            bool result = positionChecked.Verify(position, cpList, ColorsEnum.Black);

            Assert.True(result);
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("C3")]
        [InlineData("A2")]
        [InlineData("Z4")]
        public void Verify_PassUncheckedPositionByBlack_ReturnsFalse(string position)
        {
            bool result = positionChecked.Verify(position, cpList, ColorsEnum.White);

            Assert.False(result);
        }

        [Theory]
        [InlineData("B5")]
        [InlineData("E2")]
        [InlineData("C2")]
        [InlineData("Z4")]
        public void Verify_PassUncheckedPositionByWhite_ReturnsFalse(string position)
        {
            bool result = positionChecked.Verify(position, cpList, ColorsEnum.Black);

            Assert.False(result);
        }
    }
}
