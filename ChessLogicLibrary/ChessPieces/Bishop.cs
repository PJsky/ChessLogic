using System;
using System.Collections.Generic;
using System.Text;
using ChessLogicLibrary.ChessPiecePosition;

namespace ChessLogicLibrary.ChessPieces
{
    public class Bishop : StandardChessPiece
    {
        public override string Name { get; } = "Bishop";
        public Bishop(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}   
        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
