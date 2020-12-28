using ChessLogicLibrary.ChessMoveVerifiers.ChessPieceComparers;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public class KingMoveVerifier : IChessMoveVerifier
    {
        public bool Verify(IChessPiece chessPieceMoved, int finalColumnPosition, int finalRowPosition, List<IChessPiece> chessPiecesList = null)
        {
            return (IsShortMove(chessPieceMoved, finalColumnPosition, finalRowPosition) 
                && !IsPositionTakenBySameColor(chessPieceMoved, finalColumnPosition, finalRowPosition, chessPiecesList));
                
        }

        public bool IsShortMove(IChessPiece chessPieceMoved, int finalColumnPosition, int finalRowPosition)
        {
            ClosestChessPieceComparer closestChessPieceComparer = new ClosestChessPieceComparer(chessPieceMoved);
            //Check distance between current location and destination
            var distance = closestChessPieceComparer.FloatCompare(new King(0, finalColumnPosition, finalRowPosition), chessPieceMoved);
            //Return false if distance of a move is bigger than 1 horizontal + 1 vertical = sqrt(1^2 + 1^2)
            return distance > Math.Sqrt(2) ? false : true;
        }

        public bool IsPositionTakenBySameColor(IChessPiece chessPieceMoved, int finalColumnPosition, int finalRowPosition, List<IChessPiece> chessPiecesList)
        {
            if (chessPiecesList == null || chessPiecesList.Count < 1) return false;
            var chessPiecesAtFinalPosition = chessPiecesList.Where(cp => cp.Position.ColumnPosition == finalColumnPosition && cp.Position.RowPosition == finalRowPosition && cp.Color == chessPieceMoved.Color).ToList();
            return chessPiecesAtFinalPosition.Count < 1 ? false : true;
        }
    }
}
