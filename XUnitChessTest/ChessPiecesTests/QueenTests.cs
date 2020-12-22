using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class QueenTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece queen = new Queen(1, 1, 1);
            Assert.Equal("Queen", queen.Name);
        }
    }
}
