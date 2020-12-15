using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class RookTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece rook = new Rook(1, 1, 1);
            Assert.Equal("Rook", rook.Name);
        }

    }
}
