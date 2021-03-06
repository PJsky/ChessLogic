﻿using ChessLogicLibrary.ChessMoveVerifiers.ChessPieceComparers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public class KnightMoveVerifier : IChessMoveVerifier
    {
        public bool Verify(IChessPiece chessPieceMoved, int finalColumnPosition, int finalRowPosition, List<IChessPiece> chessPiecesList = null)
        {
            var closestPieceComparer = new ClosestChessPieceComparer(chessPieceMoved);
            var moveDistance = closestPieceComparer.FloatCompare(new Knight(0, finalColumnPosition, finalRowPosition), chessPieceMoved);
            
            if(moveDistance >= Math.Sqrt(8) || moveDistance <= 2f) return false;
            if (chessPiecesList == null || chessPiecesList.Count < 1) return true;
            IChessPiece pieceAtDestination = chessPiecesList.Where(cp => cp.Position.ColumnPosition == finalColumnPosition && cp.Position.RowPosition == finalRowPosition).FirstOrDefault();
            if (pieceAtDestination == null) return true;

            return pieceAtDestination.Color != chessPieceMoved.Color;
        }
    }
}
