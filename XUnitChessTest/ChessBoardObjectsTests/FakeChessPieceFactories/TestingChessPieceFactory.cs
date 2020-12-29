using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories
{
    public class TestingChessPieceFactory : IChessPieceFactory
    {
        public List<IChessPiece> GetChessPieces()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();
            cpList.Add(new Knight(0, 2, 5));
            cpList.Add(new Knight(1, 1, 2));
            cpList.Add(new Knight(1, 3, 3));

            cpList.Add(new Rook(0, 3, 2));
            cpList.Add(new Rook(0, 5, 2));


            cpList.Add(new Pawn(1, 3, 6));
            cpList.Add(new Pawn(1, 1, 4));
            cpList.Add(new Pawn(0, 2, 3));

            cpList.Add(new Bishop(0, 2, 1));

            cpList.Add(new Queen(1, 4, 1));

            return cpList;
        }
    }
}
