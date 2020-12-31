using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories
{
    public class KingCheckedTestPieceFactory : IChessPieceFactory
    {
        public List<IChessPiece> GetChessPieces()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();

            //Black
            cpList.Add(new Pawn(1, "B4"));
            cpList.Add(new Pawn(1, "D4"));

            cpList.Add(new Bishop(1, "B5"));
            cpList.Add(new King(1, "C5"));
            cpList.Add(new Queen(1, "D5"));

            //White
            cpList.Add(new Queen(0, "A2"));
            cpList.Add(new Rook(0, "C1"));

            return cpList;
        }
    }
}
