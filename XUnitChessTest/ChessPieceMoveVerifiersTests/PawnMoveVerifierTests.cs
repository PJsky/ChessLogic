using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPieceMoveVerifiersTests
{
    public class PawnMoveVerfierTests
    {
        [Fact]
        public void Verify_ValidMoveForWhite_ReturnsTrueForWhiteAndFalseForBlack()
        {
            IChessPiece whitePawn = new Pawn(0, 5, 5);
            IChessPiece blackPawn = new Pawn(1, 5, 5);

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();

            var resultWhite = pawnMoveVerifier.Verify(whitePawn, 5, 6);
            var resultBlack = pawnMoveVerifier.Verify(blackPawn, 5, 6);

            Assert.True(resultWhite);
            Assert.False(resultBlack);
        }


        [Fact]
        public void Verify_ForwardMoveBlocked_ReturnsFalse()
        {
            IChessPiece whitePawn = new Pawn(0, 5, 5);
            IChessPiece blackPawn = new Pawn(1, 5, 5);

            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(0, 5, 6));
            otherPieces.Add(new Pawn(0, 5, 4));

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();

            var resultWhite = pawnMoveVerifier.Verify(whitePawn, 5, 6, otherPieces);
            var resultBlack = pawnMoveVerifier.Verify(blackPawn, 5, 4, otherPieces);

            Assert.False(resultWhite);
            Assert.False(resultBlack);
        }

        [Fact]
        public void Verify_ValidDoubleMove_ReturnsTrue()
        {
            IChessPiece whitePawn = new Pawn(0, 5, 5);
            IChessPiece blackPawn = new Pawn(1, 5, 5);

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();

            var resultWhite = pawnMoveVerifier.Verify(whitePawn, 5, 7);
            var resultBlack = pawnMoveVerifier.Verify(blackPawn, 5, 7);

            Assert.True(resultWhite);
            Assert.False(resultBlack);
        }

        [Fact]
        public void Verify_ForwardDoubleMoveBlockedAtFinalDestination_ReturnsFalse()
        {
            IChessPiece whitePawn = new Pawn(0, 5, 5);
            IChessPiece blackPawn = new Pawn(1, 5, 5);

            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(0, 5, 7));
            otherPieces.Add(new Pawn(0, 5, 3));

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();

            var resultWhite = pawnMoveVerifier.Verify(whitePawn, 5, 7, otherPieces);
            var resultBlack = pawnMoveVerifier.Verify(blackPawn, 5, 3, otherPieces);

            Assert.False(resultWhite);
            Assert.False(resultBlack);
        }

        [Fact]
        public void Verify_ForwardDoubleMoveBlockedMidway_ReturnsFalse()
        {
            IChessPiece whitePawn = new Pawn(0, 5, 5);
            IChessPiece blackPawn = new Pawn(1, 5, 5);

            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(0, 5, 6));
            otherPieces.Add(new Pawn(0, 5, 4));

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();

            var resultWhite = pawnMoveVerifier.Verify(whitePawn, 5, 7, otherPieces);
            var resultBlack = pawnMoveVerifier.Verify(blackPawn, 5, 3, otherPieces);

            Assert.False(resultWhite);
            Assert.False(resultBlack);
        }

        [Fact]
        public void Verify_AttacksBlackWithValidData_ReturnTrue()
        {
            IChessPiece pawn = new Pawn(0, 5, 5);

            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(1, 6, 6));
            otherPieces.Add(new Pawn(1, 4, 6));

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();
            var result1 = pawnMoveVerifier.Verify(pawn, 6, 6, otherPieces);
            var result2 = pawnMoveVerifier.Verify(pawn, 4, 6, otherPieces);

            Assert.True(result1);
            Assert.True(result2);
        }

        [Fact]
        public void Verify_AttacksEmptySpace_ReturnFalse()
        {
            IChessPiece pawn = new Pawn(0, 5, 5);

            List<IChessPiece> otherPieces = new List<IChessPiece>();
            otherPieces.Add(new Pawn(1, 7, 7));

            IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();

            var result = pawnMoveVerifier.Verify(pawn, 6, 6, otherPieces);

            Assert.False(result);
        }
    }
}
