using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class RookTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece rook = new Rook(1, 1, 1);
            Assert.Equal("Rook", rook.Name);
        }

        [Theory]
        [InlineData(10, 5)]
        [InlineData(-10, 5)]
        [InlineData(5, 10)]
        [InlineData(5, -10)]
        public void Move_ValidPosition_ReturnsNewPosition(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece rook = new Rook(0, 5, 5);
            rook.Move(finalDestinationColumn, finalDestinationRow);
            Assert.Equal(finalDestinationColumn, rook.Position.ColumnPosition);
            Assert.Equal(finalDestinationRow, rook.Position.RowPosition);
        }

        [Theory]
        [InlineData(10, 6)]
        [InlineData(-10, 6)]
        [InlineData(8, 10)]
        [InlineData(2, -12)]
        public void Move_InvalidPosition_ReturnsStartingPosition(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece rook = new Rook(0, 5, 5);
            rook.Move(finalDestinationColumn, finalDestinationRow);
            Assert.Equal(5, rook.Position.ColumnPosition);
            Assert.Equal(5, rook.Position.RowPosition);
        }

    }
}
