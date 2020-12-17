using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessMoveVerifiers.ChessPieceComparers
{
    public class ClosestChessPieceComparer : IComparer<IChessPiece>
    {
        IChessPiece _mainChessPiece;
        public ClosestChessPieceComparer(IChessPiece mainChessPiece)
        {
            _mainChessPiece = mainChessPiece;
        }
        public int Compare(IChessPiece cpX, IChessPiece cpY)
        {
            //For Bishop horizontal and vertical distance to a piece must be the same
            var DistanceCpX = Math.Sqrt(Math.Pow(_mainChessPiece.Position.ColumnPosition - cpX.Position.ColumnPosition,2) + Math.Pow(_mainChessPiece.Position.RowPosition - cpX.Position.RowPosition, 2));
            var DistanceCpY = Math.Sqrt(Math.Pow(_mainChessPiece.Position.ColumnPosition - cpY.Position.ColumnPosition, 2) + Math.Pow(_mainChessPiece.Position.RowPosition - cpY.Position.RowPosition, 2));
            return (int)(DistanceCpX - DistanceCpY);
        }

        //public bool isFirstCloser(IChessPiece cpX, IChessPiece cpY)
        //{
        //    if (Compare(cpX, cpY) < 0) return true;
        //    return false;
        //}
    }
}
