using ChessLogicLibrary.ChessMoveVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class Knight : StandardChessPiece
    {

        public override string Name { get; } = "Knight";
        public Knight(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition) { }

        public override void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if (knightMoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard))
                Position.ChangePosition(columnPosition, rowPosition);
        }
        private IChessMoveVerifier knightMoveVerifier = new KnightMoveVerifier();
    }
}