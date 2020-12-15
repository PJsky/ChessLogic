using System;
using Xunit;
using ChessLogicLibrary.ChessPieces;

namespace XUnitChessTest
{
    public class PawnTests
    {
        [Fact]
        public void Constructor_PassedValidData_GivesAProperName()
        {
            IChessPiece pawn = new Pawn(1, 1, 1);
            Assert.Equal("Pawn", pawn.Name);
        }

    }
}
