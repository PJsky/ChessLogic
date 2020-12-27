using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPieceMoveVerifiersTests
{
    public class KnightMoveVerfierTests
    {
        [Theory]
        [InlineData(3, 6)]
        [InlineData(3, 4)]
        [InlineData(4, 7)]
        [InlineData(4, 3)]
        [InlineData(6, 7)]
        [InlineData(6, 3)]
        [InlineData(7, 6)]
        [InlineData(7, 4)]
        public void Verify_ValidDataForMoveWithNoOtherPieces_ReturnsTrue(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece knight = new Knight(0, 5, 5);

            IChessMoveVerifier knightMoveVerifier = new KnightMoveVerifier();

            var result = knightMoveVerifier.Verify(knight, finalDestinationColumn, finalDestinationRow);

            Assert.True(result);
            
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
        public void Verify_InvalidDataForMoveWithNoOtherPieces_ReturnsFalse(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece knight = new Knight(0, 5, 5);

            IChessMoveVerifier knightMoveVerifier = new KnightMoveVerifier();

            var result = knightMoveVerifier.Verify(knight, finalDestinationColumn, finalDestinationRow);

            Assert.False(result);

        }

        [Fact]
        public void Verify_KnightAttacksEnemy_ReturnsTrue()
        {
            IChessPiece knight = new Knight(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Knight(1,6,7));

            IChessMoveVerifier knightMoveVerifier = new KnightMoveVerifier();

            var result = knightMoveVerifier.Verify(knight, 6, 7, otherPieces);

            Assert.True(result);
        }

        [Fact]
        public void Verify_KnightAttacksSameColor_ReturnsFalse()
        {
            IChessPiece knight = new Knight(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Knight(0, 6, 7));

            IChessMoveVerifier knightMoveVerifier = new KnightMoveVerifier();

            var result = knightMoveVerifier.Verify(knight, 6, 7, otherPieces);

            Assert.False(result);
        }
    }
}
