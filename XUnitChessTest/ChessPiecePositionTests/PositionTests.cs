using System;
using Xunit;
using ChessLogicLibrary.ChessPiecePosition;

namespace XUnitChessTest
{
    public class PositionTests
    {
        [Fact]
        public void IntConstructor_PassedValidData_BuildsCorrectly()
        {
            Position position = new Position(2,3);

            Assert.Equal(2, position.ColumnPosition);
            Assert.Equal(3, position.RowPosition);
        }

        [Theory]
        [InlineData("B3", 2, 3)]
        [InlineData("A1", 1, 1)]
        [InlineData("H8", 8, 8)]
        [InlineData("a1", 1, 1)]
        [InlineData("z100", 26, 100)]
        public void StringConstructor_PassedValidData_BuildsCorrectly(string stringPosition, int finalDestinationColumn, int finalDestinationRow)
        {
            Position position = new Position(stringPosition);

            Assert.Equal(finalDestinationColumn, position.ColumnPosition);
            Assert.Equal(finalDestinationRow, position.RowPosition);
        }

        [Theory]
        [InlineData("B3")]
        [InlineData("A1")]
        [InlineData("H8")]
        [InlineData("Z100")]
        public void ToString_PassedValidData_ReturnsProperString(string stringPosition)
        {
            Position position = new Position(stringPosition);

            Assert.Equal(stringPosition, position.ToString());
        }
    }
}
