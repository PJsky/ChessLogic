using ChessLogicLibrary.ChessMoveVerifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public class Knight : StandardChessPiece
    {

        public override string Name { get; } = "Knight";
        protected override IChessMoveVerifier MoveVerifier { get; set; } = new KnightMoveVerifier();
        public Knight(int colorId, int columnPosition, int rowPosition) : base(colorId, columnPosition, rowPosition) { }
        public Knight(int colorId, string position) : base(colorId, position) { }

    }
}