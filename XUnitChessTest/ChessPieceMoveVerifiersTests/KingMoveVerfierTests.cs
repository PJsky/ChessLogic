using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPieceMoveVerifiersTests
{
    public class KingMoveVerfierTests
    {
        [Theory]
        [InlineData(4, 6)]
        [InlineData(6, 4)]
        [InlineData(6, 6)]
        [InlineData(4, 4)]
        [InlineData(6, 5)]
        [InlineData(5, 6)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        public void Verify_ValidDataForMoveWithNoOtherPieces_ReturnsTrue(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece king = new King(0, 5, 5);

            IChessMoveVerifier KingMoveVerifier = new KingMoveVerifier();

            var result = KingMoveVerifier.Verify(king, finalDestinationColumn, finalDestinationRow);

            Assert.True(result);
            
        }

        [Theory]
        [InlineData(5, 3)]
        [InlineData(5, 7)]
        [InlineData(3, 5)]
        [InlineData(7, 5)]
        [InlineData(3, 3)]
        [InlineData(3, 7)]
        [InlineData(7, 3)]
        [InlineData(7, 7)]
        public void Verify_InvalidDataForMoveWithNoOtherPieces_ReturnsFalse(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece King = new King(0, 5, 5);

            IChessMoveVerifier KingMoveVerifier = new KingMoveVerifier();

            var result = KingMoveVerifier.Verify(King, finalDestinationColumn, finalDestinationRow);

            Assert.False(result);

        }

        [Theory]
        [InlineData(4, 6)]
        [InlineData(6, 4)]
        [InlineData(6, 6)]
        [InlineData(4, 4)]
        [InlineData(6, 5)]
        [InlineData(5, 6)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        public void Verify_KingAttacksEnemy_ReturnsTrue(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece King = new King(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new King(1, finalDestinationColumn, finalDestinationRow));

            IChessMoveVerifier KingMoveVerifier = new KingMoveVerifier();

            var result = KingMoveVerifier.Verify(King, finalDestinationColumn, finalDestinationRow, otherPieces);

            Assert.True(result);
        }

        [Theory]
        [InlineData(4, 6)]
        [InlineData(6, 4)]
        [InlineData(6, 6)]
        [InlineData(4, 4)]
        [InlineData(6, 5)]
        [InlineData(5, 6)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        public void Verify_KingAttacksSameColor_ReturnsFalse(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece King = new King(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new King(0, finalDestinationColumn, finalDestinationRow));

            IChessMoveVerifier KingMoveVerifier = new KingMoveVerifier();

            var result = KingMoveVerifier.Verify(King, finalDestinationColumn, finalDestinationRow, otherPieces);

            Assert.False(result);
        }

        [Theory]
        [InlineData(4, 6)]
        [InlineData(6, 4)]
        [InlineData(6, 6)]
        [InlineData(4, 4)]
        [InlineData(6, 5)]
        [InlineData(5, 6)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        public void Verify_KingAttacksDifferentColor_ReturnsTrue(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece King = new King(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new King(1, finalDestinationColumn, finalDestinationRow));

            IChessMoveVerifier KingMoveVerifier = new KingMoveVerifier();

            var result = KingMoveVerifier.Verify(King, finalDestinationColumn, finalDestinationRow, otherPieces);

            Assert.True(result);
        }
    }
}
