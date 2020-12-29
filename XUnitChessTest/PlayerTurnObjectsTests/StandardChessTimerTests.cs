using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.PlayerTurnObjects;
using ChessLogicLibrary.PlayerTurnObjects.PlayerObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.PlayerTurnObjectsTests
{
    public class StandardChessTimerTests
    {
        [Fact]
        public void ChangeTurn() {
            IChessTimer ct = new StandardChessTimer(new StandardPlayer("A"), new StandardPlayer("B"));
            ColorsEnum startingTurnColor = ct.ColorsTurn;
            ColorsEnum secondTurnColor, thirdTurnColor;

            ct.ChangeTurn();
            secondTurnColor = ct.ColorsTurn;
            ct.ChangeTurn();
            thirdTurnColor = ct.ColorsTurn;

            Assert.Equal(ColorsEnum.White, startingTurnColor);
            Assert.Equal(ColorsEnum.Black, secondTurnColor);
            Assert.Equal(ColorsEnum.White, thirdTurnColor);
        }
    }
}
