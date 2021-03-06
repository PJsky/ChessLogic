using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class KnightTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece knight = new Knight(1, 1, 1);
            Assert.Equal("Knight", knight.Name);
        }

        [Theory]
        [InlineData(3, 6)]
        [InlineData(3, 4)]
        [InlineData(4, 7)]
        [InlineData(4, 3)]
        [InlineData(6, 7)]
        [InlineData(6, 3)]
        [InlineData(7, 6)]
        [InlineData(7, 4)]
        public void Move_ValidData_ReturnsNewPosition(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece knight = new Knight(0, 5, 5);
            knight.Move(finalDestinationColumn, finalDestinationRow);

            Assert.Equal(finalDestinationColumn, knight.Position.ColumnPosition);
            Assert.Equal(finalDestinationRow, knight.Position.RowPosition);
        }

        [Theory]
        [InlineData(4, 6)]
        [InlineData(4, 4)]
        [InlineData(5, 6)]
        [InlineData(4, 5)]
        [InlineData(6, 8)]
        [InlineData(7, 3)]
        [InlineData(8, 6)]
        [InlineData(7, 15)]
        public void Move_InvalidData_ReturnsNewPosition(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece knight = new Knight(0, 5, 5);
            knight.Move(finalDestinationColumn, finalDestinationRow);

            Assert.Equal(5, knight.Position.ColumnPosition);
            Assert.Equal(5, knight.Position.RowPosition);
        }
    }
}
