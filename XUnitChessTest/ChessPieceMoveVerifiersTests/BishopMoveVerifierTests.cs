using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPieceMoveVerifiersTests
{
    public class BishopMoveVerifierTests
    {

        [Fact] 
        public void Verify_ValidDataForMoveWithNoOtherPieces_ReturnsTrue()
        {
            IChessPiece bishop = new Bishop(0, 0, 0);
            IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();

            var result = bishopMoveVerifier.Verify(bishop, -1, -1);

            Assert.True(result);
        }

        [Fact]
        public void Verify_ValidDataForMove_ReturnsTrue()
        {
            IChessPiece bishop = new Bishop(0,5,5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();

            otherPieces.Add(new Bishop(0, 10, 10));
            otherPieces.Add(new Bishop(0, 10, 9));

            IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();

            var result = bishopMoveVerifier.Verify(bishop, 9, 9, otherPieces);

            Assert.True(result);
        }


        [Theory]
        [InlineData(10,10,20,20)]
        [InlineData(-10,-10,-20,-20)]
        [InlineData(10,-10,20,-20)]
        [InlineData(-10,10,-20,20)]
        public void Verify_OtherPieceBlocksTheWay_ReturnsFalse(int blockingColumnPosition, int blockingRowPosition,
                                                             int finalDestinationColumn, int finalDestinationRow)
        {
            IChessPiece bishop = new Bishop(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();

            otherPieces.Add(new Bishop(0, blockingColumnPosition, blockingRowPosition));
            otherPieces.Add(new Bishop(0, 10, 9));

            IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();

            var result = bishopMoveVerifier.Verify(bishop, finalDestinationColumn, finalDestinationRow, otherPieces);

            Assert.False(result);
        }

        [Fact]
        public void Verify_BishopAttacksAPiece_ReturnsTrue()
        {
            IChessPiece bishop = new Bishop(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();

            otherPieces.Add(new Bishop(1, 10, 10));
            IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();

            var result = bishopMoveVerifier.Verify(bishop, 10, 10, otherPieces);

            Assert.True(result);
        }

        [Fact]
        public void Verify_BishopAttacksSameColor_ReturnsFalse()
        {
            IChessPiece bishop = new Bishop(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();

            otherPieces.Add(new Bishop(0, 10, 10));
            IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();

            var result = bishopMoveVerifier.Verify(bishop, 10, 10, otherPieces);

            Assert.False(result);
        }
    }
}
