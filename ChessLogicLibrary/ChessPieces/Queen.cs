using ChessLogicLibrary.ChessMoveVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class Queen : StandardChessPiece
    {

        public override string Name { get; } = "Queen";
        protected override IChessMoveVerifier MoveVerifier { get; set; } = new QueenMoveVerifier();
        public Queen(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition){}
        public Queen(int colorId, string position) : base(colorId, position) { }
    }
}
