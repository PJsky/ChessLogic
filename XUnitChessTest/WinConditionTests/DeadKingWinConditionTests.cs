using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using ChessLogicLibrary.WinConditionsVerifiers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories;

namespace XUnitChessTest.WinConditionTests
{
    public class DeadKingWinConditionTests
    {
        private IChessPieceFactory cpFactory;
        IChessBoard chessBoard;
        IWinCondition deadKingWinCondition;

        private void Setup()
        {
            cpFactory = new KingsToKillChessPieceFactory();
            chessBoard = new StandardChessBoard(cpFactory);
            deadKingWinCondition = new DeadKingCondition(chessBoard.ChessPiecesOnBoard);
        }

        [Fact]
        public void Verify_WhiteKingKillsBlackKing_ReturnsWhiteAsWinner()
        {
            Setup();

            chessBoard.MoveAPiece("A1", "B2");

            ColorsEnum? result = deadKingWinCondition.Verify();

            Assert.Equal(ColorsEnum.White, result);
        }

        [Fact]
        public void Verify_BlackKingKillsWhiteKing_ReturnsWhiteAsWinner()
        {
            Setup();

            chessBoard.MoveAPiece("B2", "A1");

            ColorsEnum? result = deadKingWinCondition.Verify();

            Assert.Equal(ColorsEnum.Black, result);
        }

        [Fact]
        public void Verify_NothingHappens_ReturnsNull()
        {
            Setup();

            ColorsEnum? result = deadKingWinCondition.Verify();

            Assert.Null(result);
        }
    }
}
