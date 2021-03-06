﻿using ChessLogicLibrary.ChessMoveVerifiers;
using ChessLogicLibrary.ChessPiecePosition;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPieces
{
    public abstract class StandardChessPiece : IChessPiece
    {
        public abstract string Name { get; }
        public bool wasMoved { get; protected set; } = false;
        public StandardChessPiece(int colorId, int columnPosition, int rowPosition)
        {
            Color = (ColorsEnum)colorId;
            Position = new Position(columnPosition, rowPosition);
        }

        public StandardChessPiece(int colorId, string position)
        {
            Color = (ColorsEnum)colorId;
            Position = new Position(position);
        }
        public ColorsEnum Color { get; set; }
        public Position Position { get; protected set; }
        public virtual bool Move(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if (MoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard)) 
            { 
                Position.ChangePosition(columnPosition, rowPosition);
                return true;
            }
            return false;
        }

        public virtual bool IsMovePossible(int columnPosition, int rowPosition, List<IChessPiece> chessPiecesOnBoard = null)
        {
            if (MoveVerifier.Verify(this, columnPosition, rowPosition, chessPiecesOnBoard))return true;
            return false;
        }

        protected abstract IChessMoveVerifier MoveVerifier { get; set; }

    }
}
