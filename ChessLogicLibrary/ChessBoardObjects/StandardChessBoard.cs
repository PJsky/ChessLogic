using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.ChessBoardObjects
{
    public class StandardChessBoard : IChessBoard
    {
        //Has a list of chess pieces with their positions
        public List<IChessPiece> chessPiecesOnBoard { get; } = new List<IChessPiece>();
        public StandardChessBoard(IChessPieceFactory CpFactory)
        {
            chessPiecesOnBoard = CpFactory.GetChessPieces();
        }

        public bool MoveAPiece(string startingPositionString, string finalPositionString)
        {
            Position startingPosition = new Position(startingPositionString);
            Position finalPosition = new Position(finalPositionString);
            IChessPiece chessPieceMoved = chessPiecesOnBoard.Where(cp => cp.Position.ColumnPosition == startingPosition.ColumnPosition
                                                                 && cp.Position.RowPosition == startingPosition.RowPosition)
                                                                 .First();
            bool wasPieceMoved = chessPieceMoved.Move(finalPosition.ColumnPosition, finalPosition.RowPosition, chessPiecesOnBoard);
            if (wasPieceMoved)
            {
                chessPiecesOnBoard.RemoveAll(cp => cp.Position.ColumnPosition == finalPosition.ColumnPosition
                                             && cp.Position.RowPosition == finalPosition.RowPosition 
                                             && cp != chessPieceMoved);
                return true;
            }
            return false;
        }
    }
}
