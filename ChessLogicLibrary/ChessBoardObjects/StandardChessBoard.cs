﻿using ChessLogicLibrary.ChessPiecePosition;
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
        public Position UpperRightCorner { get; } = new Position("H8");
        public StandardChessBoard(IChessPieceFactory CpFactory)
        {
            ChessPiecesOnBoard = CpFactory.GetChessPieces();
        }

        public bool MoveAPiece(string startingPositionString, string finalPositionString)
        {
            if (!IsWithinBoundaries(startingPositionString) || !IsWithinBoundaries(finalPositionString)) return false;
            Position finalPosition = new Position(finalPositionString);
            IChessPiece chessPieceMoved = GetAPieceFromPosition(startingPositionString);
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
                                                                 .FirstOrDefault();
            return chessPieceSearched;
        }

        public bool IsWithinBoundaries(string positionString)
        {
            Position position = new Position(positionString);

            bool IsWithinWidthBoundary = position.ColumnPosition <= UpperRightCorner.ColumnPosition && position.ColumnPosition > 0;
            bool IsWithinHeightBoundary = position.RowPosition <= UpperRightCorner.RowPosition && position.RowPosition > 0;

            return IsWithinWidthBoundary && IsWithinHeightBoundary;
        }
    }
}
