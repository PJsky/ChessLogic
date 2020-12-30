using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories
{
    public class KingsToKillChessPieceFactory : IChessPieceFactory
    {
        public List<IChessPiece> GetChessPieces()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();

            cpList.Add(new King(0, 1, 1));
            cpList.Add(new King(1, 2, 2));

            return cpList;
        }
    }
}
