using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessBoardObjects
{
    public class ChessBoard
    {
        //Has a list of chess pieces with their positions
        IChessPieceFactory cpFactory;
        List<IChessPiece> chessPiecesOnBoard = new List<IChessPiece>();
        public ChessBoard(IChessPieceFactory CpFactory)
        {
            cpFactory = CpFactory;
        }
        //MoveAPiece() == makes a move on a board given only 2 positions
        //If position is taken by other piece removes it

    }
}
