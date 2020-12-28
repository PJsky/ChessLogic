using ChessLogicLibrary.ChessMoveVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class Pawn : StandardChessPiece
    {

        public override string Name { get; } = "Pawn";
        public Pawn(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}
        public Pawn(int colorId, string position) : base(colorId, position) { }

        public override void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if (pawnMoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard)) 
            {
                Position.ChangePosition(columnPosition, rowPosition);
                wasMoved = true;
            }
        }
        private IChessMoveVerifier pawnMoveVerifier = new PawnMoveVerifier();
    }
}
