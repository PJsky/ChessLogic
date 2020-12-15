using System;
using Xunit;
using ChessLogicLibrary.ChessPiecePosition;

namespace XUnitChessTest
{
    public class PositionTests
    {
        //Obsolete, constructor is shared between the classes due to implementation of abstract class which holdes shared data
        [Fact]
        public void Constructor_PassedValidData_BuildsCorrectly()
        {
            Position position = new Position(2,3);

            Assert.Equal(2, position.ColumnPosition);
            Assert.Equal(3, position.RowPosition);
        }

    }
}
