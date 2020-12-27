using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class KingTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece king = new King(1, 1, 1);
            Assert.Equal("King", king.Name);
        }

        [Theory]
        [InlineData(5, 4)]
        [InlineData(5, 6)]
        [InlineData(4, 5)]
        [InlineData(6, 5)]
        [InlineData(4, 4)]
        [InlineData(4, 6)]
        [InlineData(6, 4)]
        [InlineData(6, 6)]
        public void Move_PassedValidPosition_RetrunsNewPosition(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece king = new King(0, 5, 5);
            king.Move(finalDestinationColumn, finalDestinationRow);

            Assert.Equal(finalDestinationColumn, king.Position.ColumnPosition);
            Assert.Equal(finalDestinationRow, king.Position.RowPosition);
        }
        [Theory]
        [InlineData(5, 3)]
        [InlineData(5, 7)]
        [InlineData(3, 5)]
        [InlineData(7, 5)]
        [InlineData(3, 3)]
        [InlineData(3, 7)]
        [InlineData(7, 3)]
        [InlineData(7, 7)]
        public void Move_PassedInvalidPosition_RetrunsOldPosition(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece king = new King(0, 5, 5);
            king.Move(finalDestinationColumn, finalDestinationRow);

            Assert.Equal(5, king.Position.ColumnPosition);
            Assert.Equal(5, king.Position.RowPosition);
        }
    }
}
