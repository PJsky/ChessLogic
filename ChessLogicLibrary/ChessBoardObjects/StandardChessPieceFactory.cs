using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessBoardObjects
{
    public class StandardChessPieceFactory : IChessPieceFactory
    {
        public List<IChessPiece> GetChessPieces()
        {
            List<IChessPiece> chessPieces = new List<IChessPiece>();
            chessPieces.AddRange(getPawns());
            chessPieces.AddRange(getRooks());
            chessPieces.AddRange(getKnights());
            chessPieces.AddRange(getBishops());
            chessPieces.AddRange(getQueens());
            chessPieces.AddRange(getKings());

            return chessPieces;
        }

        private List<IChessPiece> getPawns()
        {
            List<IChessPiece> pawns = new List<IChessPiece>();
            for (int i = 1; i <= 8; i++)
            {
                pawns.Add(new Pawn(0, i, 2));
                pawns.Add(new Pawn(1, i, 7));
            }
            return pawns;
        }

        private List<IChessPiece> getRooks()
        {
            List<IChessPiece> rooks = new List<IChessPiece>();
            rooks.Add(new Rook(0, 1, 1));
            rooks.Add(new Rook(0, 8, 1));
            rooks.Add(new Rook(1, 1, 8));
            rooks.Add(new Rook(1, 8, 8));
            return rooks;
        }

        private List<IChessPiece> getKnights()
        {
            List<IChessPiece> knights = new List<IChessPiece>();
            knights.Add(new Knight(0, 2, 1));
            knights.Add(new Knight(0, 7, 1));
            knights.Add(new Knight(1, 2, 8));
            knights.Add(new Knight(1, 7, 8));
            return knights;
        }

        private List<IChessPiece> getBishops()
        {
            List<IChessPiece> bishops = new List<IChessPiece>();
            bishops.Add(new Bishop(0, 3, 1));
            bishops.Add(new Bishop(0, 6, 1));
            bishops.Add(new Bishop(1, 3, 8));
            bishops.Add(new Bishop(1, 6, 8));
            return bishops;
        }

        private List<IChessPiece> getQueens()
        {
            List<IChessPiece> queens = new List<IChessPiece>();
            queens.Add(new Queen(0, 4, 1));
            queens.Add(new Queen(1, 4, 8));
            return queens;
        }

        private List<IChessPiece> getKings()
        {
            List<IChessPiece> kings = new List<IChessPiece>();
            kings.Add(new King(0, 5, 1));
            kings.Add(new King(1, 5, 8));
            return kings;
        }
    }
}
