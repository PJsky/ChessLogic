using ChessLogicLibrary.ChessBoardObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories;

namespace XUnitChessTest.ChessBoardObjectsTests
{
    public class StandardChessBoardTests
    {
        [Theory]
        [InlineData("B1", "A2")]
        [InlineData("D1", "B1")]
        [InlineData("D1", "C2")]
        [InlineData("D1", "E2")]
        [InlineData("C2", "C3")]
        [InlineData("C2", "A2")]
        [InlineData("C3", "B1")]
        [InlineData("C3", "E2")]
        [InlineData("C3", "B5")]
        [InlineData("B5", "C3")]
        [InlineData("C6", "B5")]
        [InlineData("B3", "A4")]
        public void MoveAPiece_PieceAttacksOtherPiece_ReturnsTrueRemovesOnePiece(string startingPosition, string finalPosition)
        {
            IChessBoard chessBoard = new StandardChessBoard(new TestingChessPieceFactory());
            int startingAmountOfPieces = chessBoard.ChessPiecesOnBoard.Count;

            var hasMoveBeenMade = chessBoard.MoveAPiece(startingPosition, finalPosition);
            var finalAmountOfPieces = chessBoard.ChessPiecesOnBoard.Count;
            bool wasOnePieceTaken = startingAmountOfPieces - finalAmountOfPieces == 1;

            Assert.True(hasMoveBeenMade);
            Assert.True(wasOnePieceTaken);
        }

        [Theory]
        [InlineData("H1", "Z1")]
        [InlineData("H8", "P16")]
        [InlineData("A8", "A16")]
        public void MoveAPiece_PieceTriesToGetOutOfBoard_ReturnsFalse(string startingPosition, string finalPosition)
        {
            IChessBoard chessBoard = new StandardChessBoard(new TestingChessPieceFactory());
            bool escapeResult = chessBoard.MoveAPiece(startingPosition, finalPosition);

            Assert.False(escapeResult);

        }
    }
}
