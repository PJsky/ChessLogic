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
        public List<IChessPiece> ChessPiecesOnBoard { get; } = new List<IChessPiece>();
        public StandardChessBoard(IChessPieceFactory CpFactory)
        {
            ChessPiecesOnBoard = CpFactory.GetChessPieces();
        }

        //Add check whether a apiece is inside chessboard
        public bool MoveAPiece(string startingPositionString, string finalPositionString)
        {
            Position startingPosition = new Position(startingPositionString);
            Position finalPosition = new Position(finalPositionString);
            IChessPiece chessPieceMoved = ChessPiecesOnBoard.Where(cp => cp.Position.ColumnPosition == startingPosition.ColumnPosition
                                                                 && cp.Position.RowPosition == startingPosition.RowPosition)
                                                                 .First();
            bool wasPieceMoved = chessPieceMoved.Move(finalPosition.ColumnPosition, finalPosition.RowPosition, ChessPiecesOnBoard);
            if (wasPieceMoved)
            {
                ChessPiecesOnBoard.RemoveAll(cp => cp.Position.ColumnPosition == finalPosition.ColumnPosition
                                             && cp.Position.RowPosition == finalPosition.RowPosition 
                                             && cp != chessPieceMoved);
                return true;
            }
            return false;
        }

        public IChessPiece GetAPieceFromPosition(string positionString)
        {
            Position positon = new Position(positionString);
            IChessPiece chessPieceSearched = ChessPiecesOnBoard.Where(cp => cp.Position.ColumnPosition == positon.ColumnPosition
                                                                 && cp.Position.RowPosition == positon.RowPosition)
                                                                 .First();
            return chessPieceSearched;
        }
    }
}
