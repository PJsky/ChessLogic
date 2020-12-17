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
        public void Verify_OtherPieceBlockTheWay_ReturnsTrue()
        {
            IChessPiece bishop = new Bishop(0, 5, 5);
            List<IChessPiece> otherPieces = new List<IChessPiece>();

            otherPieces.Add(new Bishop(0, 10, 10));
            otherPieces.Add(new Bishop(0, 10, 9));

            IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();

            var result = bishopMoveVerifier.Verify(bishop, 20, 20, otherPieces);

            Assert.False(result);
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
