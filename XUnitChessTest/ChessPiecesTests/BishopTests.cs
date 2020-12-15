using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class BishopTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece bishop = new Bishop(1, 1, 1);
            Assert.Equal("Bishop", bishop.Name);
        }

    }
}
