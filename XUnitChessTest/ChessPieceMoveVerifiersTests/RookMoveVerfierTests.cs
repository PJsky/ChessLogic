using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPieceMoveVerifiersTests
{
    public class RookMoveVerfierTests
    {
        [Theory]
        [InlineData(5, 0)]
        [InlineData(0, 5)]
        [InlineData(-5, 0)]
        [InlineData(0, -5)]
        public void Verify_ValidDataForMoveWithNoOtherPieces_ReturnsTrue(int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece rook = new Rook(0, 0, 0);

            IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();

            var result = rookMoveVerifier.Verify(rook, finalDestinationColumn, finalDestinationRow);

            Assert.True(result);
            
        }

        [Fact] 
        public void Verify_DiagonalMoveWithNoOtherPieces_ReturnsFalse()
        {
            IChessPiece rook = new Rook(0, 5, 5);
            IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();

            var result = rookMoveVerifier.Verify(rook, 10, 10);

            Assert.False(result);
        }

        [Theory]
        [InlineData(4, 5, 0, 5)]
        [InlineData(5, 4, 5, 0)]
        [InlineData(6, 5, 10, 5)]
        [InlineData(5, 6, 5, 10)]
        public void Verify_OtherPieceBlocksTheWay(int blockingColumnPosition, int blockingRowPosition,
                                                  int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece rook = new Rook(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();

            otherPieces.Add(new Rook(0, blockingColumnPosition, blockingRowPosition));
            otherPieces.Add(new Rook(0, 10, 10));

            IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();

            var result = rookMoveVerifier.Verify(rook, finalDestinationColumn, finalDestinationRow, otherPieces);

            Assert.False(result);
        }

        [Fact]
        public void Verify_RookAttacksAPiece_ReturnsTrue()
        {
            IChessPiece rook = new Rook(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Rook(1, 10, 5));

            IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();

            var result = rookMoveVerifier.Verify(rook, 10, 5, otherPieces);

            Assert.True(result);
        }

        [Fact]
        public void Verify_RookAttacksSameColor_ReturnsFalse()
        {
            IChessPiece rook = new Rook(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Rook(0, 10, 5));

            IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();

            var result = rookMoveVerifier.Verify(rook, 10, 5, otherPieces);

            Assert.False(result);
        }

    }
}
