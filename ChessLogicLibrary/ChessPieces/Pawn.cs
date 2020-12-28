using ChessLogicLibrary.ChessMoveVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class Pawn : StandardChessPiece
    {

        public override string Name { get; } = "Pawn";
        protected override IChessMoveVerifier MoveVerifier { get; set; } = new PawnMoveVerifier();
        public Pawn(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}
        public Pawn(int colorId, string position) : base(colorId, position) { }
    }
}
