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
        public override void Move(int columnPosition, int rowPosition)
        {
            if(isPossibleMove(columnPosition, rowPosition))
                Position.ChangePosition(columnPosition, rowPosition);
        }

        private bool isPossibleMove(int columnPosition, int rowPosition)
        {
            return true;
        }
    }
}
