using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class KnightTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece knight = new Knight(1, 1, 1);
            Assert.Equal("Knight", knight.Name);
        }

    }
}
