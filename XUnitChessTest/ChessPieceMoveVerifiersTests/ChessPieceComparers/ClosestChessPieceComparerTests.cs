using ChessLogicLibrary.ChessMoveVerifiers.ChessPieceComparers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessPieceMoveVerifiersTests.ChessPieceComparers
{
    public class ClosestChessPieceComparerTests
    {
        [Fact]
        public void ListCompare_FirstValueCloser_ReturnsFirstValue()
        {
            IChessPiece mainChessPiece = new Bishop(0, 4, 4);
            IComparer<IChessPiece> closestChessPieceComparer = new ClosestChessPieceComparer(mainChessPiece);
            var firstPiece = new Bishop(0, 5, 5);
            var secondPiece = new Bishop(0, 6, 6);
            List<IChessPiece> comparedPieces = new List<IChessPiece>();
            comparedPieces.Add(firstPiece);
            comparedPieces.Add(secondPiece);

            var closestToTheMainPiece = comparedPieces.OrderBy(cp => cp, closestChessPieceComparer).First();
            bool isFirstValueCloser = closestChessPieceComparer.Compare(firstPiece, secondPiece) < 0;  

            Assert.Equal(firstPiece, closestToTheMainPiece);
            Assert.True(isFirstValueCloser);
        }

        [Fact]
        public void ListCompare_SecondValueCloser_ReturnsSecondValue()
        {
            IChessPiece mainChessPiece = new Bishop(0, 5, 5);
            IComparer<IChessPiece> closestChessPieceComparer = new ClosestChessPieceComparer(mainChessPiece);
            var firstPiece = new Bishop(0, 22, 22);
            var secondPiece = new Bishop(0, 12, 12);
            List<IChessPiece> comparedPieces = new List<IChessPiece>();
            comparedPieces.Add(firstPiece);
            comparedPieces.Add(secondPiece);

            var closestToTheMainPiece = comparedPieces.OrderBy(cp => cp, closestChessPieceComparer).First();
            bool isSecondValueCloser = closestChessPieceComparer.Compare(firstPiece, secondPiece) > 0;

            Assert.Equal(secondPiece, closestToTheMainPiece);
            Assert.True(isSecondValueCloser);
        }
    }
}
