using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitChessTest.ChessBoardObjectsTests.FakeChessPieceFactories
{
    public class KingCheckMatedTestPieceFactory : IChessPieceFactory
    {
        private int settingNumber;
        public KingCheckMatedTestPieceFactory(int SettingNumber)
        {
            settingNumber = SettingNumber;
        }
        public List<IChessPiece> GetChessPieces()
        {
            switch (settingNumber)
            {
                case 1:
                    return GetChessPiecesSetOne();
                case 2:
                    return GetChessPiecesSetTwo();
                case 3:
                    return GetChessPiecesSetThree();
                case 4:
                    return GetChessPiecesSetFour();
                default:
                    return new List<IChessPiece>();
            }
        }

        private List<IChessPiece> GetChessPiecesSetOne()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();


            cpList.Add(new King(0, "A1"));

            cpList.Add(new Queen(1, "A2"));
            cpList.Add(new Rook(1, "A5"));

            return cpList;
        }
        private List<IChessPiece> GetChessPiecesSetTwo()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();


            cpList.Add(new King(0, "D4"));

            cpList.Add(new Queen(1, "C3"));
            cpList.Add(new Queen(1, "E5"));

            return cpList;
        }
        private List<IChessPiece> GetChessPiecesSetThree()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();


            cpList.Add(new King(0, "C3"));

            cpList.Add(new Knight(1, "D3"));
            cpList.Add(new Queen(1, "C4"));
            cpList.Add(new Rook(1, "C6"));
            cpList.Add(new Rook(1, "B5"));

            return cpList;
        }
        private List<IChessPiece> GetChessPiecesSetFour()
        {
            List<IChessPiece> cpList = new List<IChessPiece>();


            cpList.Add(new King(0, "A1"));

            cpList.Add(new Queen(1, "A2"));
            cpList.Add(new Rook(1, "A5"));

            cpList.Add(new Rook(0, "D2"));

            return cpList;
        }

    }
}
