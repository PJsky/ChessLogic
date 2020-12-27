using ChessLogicLibrary.ChessMoveVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class King : StandardChessPiece
    {
        public override string Name { get; } = "King";
        public King(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}

        public override void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if (kingMoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard))
                Position.ChangePosition(columnPosition, rowPosition);
        }
        private IChessMoveVerifier kingMoveVerifier = new KingMoveVerifier();
    }
}
