using System;
using System.Collections.Generic;
using System.Text;
using ChessLogicLibrary.ChessMoveVerifiers;

namespace ChessLogicLibrary.ChessPieces
{
    public class Rook : StandardChessPiece
    {

        public override string Name { get; } = "Rook";
        public Rook(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}
        public Rook(int colorId, string position) : base(colorId, position) { }

        public override void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if (rookMoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard)) { 
                Position.ChangePosition(columnPosition, rowPosition);
                wasMoved = true;
            }
        }
        private IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();
    }
}
