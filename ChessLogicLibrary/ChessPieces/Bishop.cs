using System;
using System.Collections.Generic;
using System.Text;
using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPiecePosition;

namespace ChessLogicLibrary.ChessPieces
{
    public class Bishop : StandardChessPiece
    {
        public override string Name { get; } = "Bishop";
        public Bishop(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition) { }
        public Bishop(int colorId, string position) : base(colorId, position) { }
        public override void Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if(isPossibleMove(columnPosition, rowPosition, chessPiecesOnBoard))
                Position.ChangePosition(columnPosition, rowPosition);
        }

        private bool isPossibleMove(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        { 
           return bishopMoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard);
        }
        private IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();
    }
}
 