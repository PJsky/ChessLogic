using System;
using Xunit;
using ChessLogicLibrary.ChessPiecePosition;

namespace XUnitChessTest
{
    public class PositionTests
    {
        [Fact]
        public void Constructor_PassedValidData_BuildsCorrectly()
        {
            Position position = new Position(2,3);

            Assert.Equal(2, position.ColumnPosition);
            Assert.Equal(3, position.RowPosition);
        }

        [Fact]
        public void ChangePostion_MovedToDifferentCoordinates_ChangesCoorditantesProperly()
        {
            Position position = new Position(1, 1);
            
            position.ChangePosition(2, 3);

            Assert.Equal(2, position.ColumnPosition);
            Assert.Equal(3, position.RowPosition);
        }
    }
}
