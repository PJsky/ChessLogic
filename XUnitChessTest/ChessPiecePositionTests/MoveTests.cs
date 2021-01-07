using ChessLogicLibrary.ChessPiecePosition;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPiecePositionTests
{
    public class MoveTests
    {
        [Fact]
        public void Contructor_ValidData_BuildsProperly()
        {
            Move moveOne = new Move("A2", "A4");

            Position startingPositionTwo = new Position("A2");
            Position finalPositionTwo = new Position("A4");
            Move moveTwo = new Move(startingPositionTwo, finalPositionTwo);

            Move moveThree = new Move();
            moveThree.StartingPosition = new Position("A2");
            moveThree.FinalPosition = new Position("A4");

            Assert.Equal(1, moveOne.StartingPosition.ColumnPosition);
            Assert.Equal(1, moveTwo.StartingPosition.ColumnPosition);
            Assert.Equal(1, moveThree.StartingPosition.ColumnPosition);

            Assert.Equal(2, moveOne.StartingPosition.RowPosition);
            Assert.Equal(2, moveTwo.StartingPosition.RowPosition);
            Assert.Equal(2, moveThree.StartingPosition.RowPosition);

            Assert.Equal(1, moveOne.FinalPosition.ColumnPosition);
            Assert.Equal(1, moveTwo.FinalPosition.ColumnPosition);
            Assert.Equal(1, moveThree.FinalPosition.ColumnPosition);

            Assert.Equal(4, moveOne.FinalPosition.RowPosition);
            Assert.Equal(4, moveTwo.FinalPosition.RowPosition);
            Assert.Equal(4, moveThree.FinalPosition.RowPosition);
        }
    }
}
