﻿using ChessLogicLibrary.ChessPieces;
using System.Collections.Generic;

namespace ChessLogicLibrary.ChessBoardObjects
{
    public interface IChessBoard
    {
        List<IChessPiece> ChessPiecesOnBoard { get; }
        bool MoveAPiece(string startingPositionString, string finalPositionString);
        IChessPiece GetAPieceFromPosition(string positionString);
    }
}