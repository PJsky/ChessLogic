using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class BishopTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece bishop = new Bishop(1, 1, 1);
            Assert.Equal("Bishop", bishop.Name);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(7, 1)]
        [InlineData(1, 7)]
        [InlineData(7, 7)]
        public void Move_ValidPosition_ReturnsNewPosition(int columnPosition, int rowPosition)
        {
            IChessPiece bishop = new Bishop(0, 4, 4);
            bishop.Move(columnPosition, rowPosition);

            Assert.Equal(bishop.Position.ColumnPosition, columnPosition);
            Assert.Equal(bishop.Position.RowPosition, rowPosition);
        }

    }
}
