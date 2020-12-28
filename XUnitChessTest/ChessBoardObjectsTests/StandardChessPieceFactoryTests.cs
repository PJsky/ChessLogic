using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessBoardObjectsTests
{
    public class StandardChessPieceFactoryTests
    {
        [Fact]
        public void GetChessPieces_ReturnsListOfAllStandardChessBoardPieces()
        {
            StandardChessPieceFactory cpFactory = new StandardChessPieceFactory();
            List<IChessPiece> chessPieces = cpFactory.GetChessPieces();

            //Get amount of pieces
            var amountOfPawns = chessPieces.Where(cp => cp.Name == "Pawn").ToList();
            var amountOfRooks = chessPieces.Where(cp => cp.Name == "Rook").ToList();
            var amountOfKnights = chessPieces.Where(cp => cp.Name == "Knight").ToList();
            var amountOfBishops = chessPieces.Where(cp => cp.Name == "Bishop").ToList();
            var amountOfQueens = chessPieces.Where(cp => cp.Name == "Queen").ToList();
            var amountOfKings = chessPieces.Where(cp => cp.Name == "King").ToList();

            //Get amouts of each colored pieces
            var amountOfWhitePieces = chessPieces.Where(cp => cp.Color == ColorsEnum.White).ToList();
            var amountOfBlackPieces = chessPieces.Where(cp => cp.Color == ColorsEnum.Black).ToList();

            Assert.Equal(16, amountOfPawns.Count);
            Assert.Equal(4, amountOfRooks.Count);
            Assert.Equal(4, amountOfKnights.Count);
            Assert.Equal(4, amountOfBishops.Count);
            Assert.Equal(2, amountOfQueens.Count);
            Assert.Equal(2, amountOfKings.Count);
            Assert.Equal(16, amountOfWhitePieces.Count);
            Assert.Equal(16, amountOfBlackPieces.Count);
        }
    }
}
