using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class Queen : StandardChessPiece
    {

        public override string Name { get; } = "Queen";
        public Queen(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}

        public override void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            throw new NotImplementedException();
        }
    }
}
