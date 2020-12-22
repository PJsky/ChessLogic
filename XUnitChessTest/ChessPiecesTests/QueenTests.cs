using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class QueenTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece queen = new Queen(1, 1, 1);
            Assert.Equal("Queen", queen.Name);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 1)]
        [InlineData(1, 5)]
        public void Move_ValidPosition_ReturnsNewPosition(int columnPosition, int rowPosition)
        {
            IChessPiece queen = new Queen(0, 5, 5);
            queen.Move(columnPosition, rowPosition);

            Assert.Equal(columnPosition, queen.Position.ColumnPosition);
            Assert.Equal(rowPosition, queen.Position.RowPosition);
        }
    }
}
