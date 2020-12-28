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
        protected override IChessMoveVerifier MoveVerifier { get; set; } = new BishopMoveVerifier();

        public Bishop(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition) { }
        public Bishop(int colorId, string position) : base(colorId, position) { }
    }
}
 