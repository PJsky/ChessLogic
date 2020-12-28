using System;
using System.Collections.Generic;
using System.Text;
using ChessLogicLibrary.ChessMoveVerifiers;

namespace ChessLogicLibrary.ChessPieces
{
    public class Rook : StandardChessPiece
    {

        public override string Name { get; } = "Rook";
        protected override IChessMoveVerifier MoveVerifier { get; set; } = new RookMoveVerifier();
        public Rook(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}
        public Rook(int colorId, string position) : base(colorId, position) { }
    }
}
