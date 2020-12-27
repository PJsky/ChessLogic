using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class PawnTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece pawn = new Pawn(1, 1, 1);
            Assert.Equal("Pawn", pawn.Name);
        }

        [Fact]
        public void MoveOneTile_ValidPosition_ReturnsNewPosition()
        {
            IChessPiece pawn = new Pawn(0, 5, 5);
            pawn.Move(5, 6);

            Assert.Equal(6, pawn.Position.RowPosition);
        }
        [Fact]
        public void MoveTwoTiles_ValidPosition_ReturnsNewPosition()
        {
            IChessPiece pawn = new Pawn(0, 5, 5);
            pawn.Move(5, 7);

            Assert.Equal(7, pawn.Position.RowPosition);
        }

    }
}
