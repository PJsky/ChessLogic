using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest
{
    public class StrandardChessPieceTests 
    {

        [Fact]
        public void Constructor_PassedValidData_BuildsCorrectly()
        {
            IChessPiece bishopWhite = new Bishop(0, 3, 1);
            IChessPiece kingBlack = new King(1, 3, 8);

            Assert.Equal(ColorsEnum.White, bishopWhite.Color);
            Assert.Equal(3, bishopWhite.Position.ColumnPosition);
            Assert.Equal(1, bishopWhite.Position.RowPosition);

            Assert.Equal(ColorsEnum.Black, kingBlack.Color);
            Assert.Equal(3, kingBlack.Position.ColumnPosition);
            Assert.Equal(8, kingBlack.Position.RowPosition);
        }

        [Fact]
        public void StringPositionConstructor_PassedValidData_BuildsCorrectly()
        {
            IChessPiece bishopWhite = new Bishop(0, "C1");
            IChessPiece kingBlack = new King(1, "C8");

            Assert.Equal(ColorsEnum.White, bishopWhite.Color);
            Assert.Equal(3, bishopWhite.Position.ColumnPosition);
            Assert.Equal(1, bishopWhite.Position.RowPosition);

            Assert.Equal(ColorsEnum.Black, kingBlack.Color);
            Assert.Equal(3, kingBlack.Position.ColumnPosition);
            Assert.Equal(8, kingBlack.Position.RowPosition);
        }

    }
}
