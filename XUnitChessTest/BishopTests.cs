using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class BishopTests
    {
        [Fact]
        public void Constructor_PassedValidData_BuildsCorrectly()
        {
            IChessPiece bishopWhite = new Bishop(0, 3, 1);
            IChessPiece bishopBlack = new Bishop(1, 3, 8);
            
            Assert.Equal(ColorsEnum.White, bishopWhite.Color);
            Assert.Equal(3, bishopWhite.Position.ColumnPosition);
            Assert.Equal(1, bishopWhite.Position.RowPosition);

            Assert.Equal(ColorsEnum.Black, bishopBlack.Color);
            Assert.Equal(3, bishopBlack.Position.ColumnPosition);
            Assert.Equal(8, bishopBlack.Position.RowPosition);
        }
    }
}
