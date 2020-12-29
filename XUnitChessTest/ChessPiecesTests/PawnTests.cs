using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;
using System.Collections.Generic;

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

        [Fact]
        public void Move_WhiteAttackBlack_ReturnsTrue()
        {
            IChessPiece pawn = new Pawn(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(1, 4, 6));
            otherPieces.Add(new Pawn(1, 6, 6));

            bool firstAttackResult = pawn.Move(4, 6, otherPieces);
            pawn = new Pawn(0, 5, 5);
            bool secondAttackResult = pawn.Move(4, 6, otherPieces);

            Assert.True(firstAttackResult);
            Assert.True(secondAttackResult);
        }

        [Fact]
        public void Move_BlackAttacksWhite_ReturnsTrue()
        {
            IChessPiece pawn = new Pawn(1, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(0, 4, 4));
            otherPieces.Add(new Pawn(0, 6, 4));

            bool firstAttackResult = pawn.Move(4, 4, otherPieces);
            pawn = new Pawn(1, 5, 5);
            bool secondAttackResult = pawn.Move(6, 4, otherPieces);

            Assert.True(firstAttackResult);
            Assert.True(secondAttackResult);
        }

    }
}
