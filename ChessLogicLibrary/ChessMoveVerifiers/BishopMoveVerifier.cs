using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChessLogicLibrary.ChessMoveVerifiers.ChessPieceComparers;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public class BishopMoveVerifier : IChessMoveVerifier
    {
        public bool Verify(IChessPiece chessPieceMoved,
                           int finalColumnPosition, int finalRowPosition,
                           List<IChessPiece> chessPiecesList = null)
        {
            chessPiecesList = chessPiecesList ?? new List<IChessPiece>();
            if (!IsBishopMovement(chessPieceMoved, finalColumnPosition, finalRowPosition) ||
                IsOtherPieceBlockingMove(chessPieceMoved,finalColumnPosition,finalRowPosition,chessPiecesList)) 
                return false;
            return true;
            
        }

        private bool IsBishopMovement(IChessPiece chessPieceMoved,
                                      int finalColumnPosition, int finalRowPosition)
        {
            //Check if it actually moves
            if (chessPieceMoved.Position.ColumnPosition == finalColumnPosition 
                && chessPieceMoved.Position.RowPosition == finalRowPosition) 
                return false;

            var horizontalMovement = Math.Abs(chessPieceMoved.Position.ColumnPosition - finalColumnPosition);
            var verticalMovement = Math.Abs(chessPieceMoved.Position.RowPosition - finalRowPosition);

            if (horizontalMovement != verticalMovement) return false;
            return true;
        }

        private bool IsOtherPieceBlockingMove(IChessPiece chessPieceMoved,
                                        int finalColumnPosition, int finalRowPosition,
                                        List<IChessPiece> chessPiecesList)
        {
            //Check if there are pieces on board
            if (chessPiecesList.Count < 1) return false;
            
            //Filter all the ones not in the way
            var PiecesInTheWay = chessPiecesList.Where(cp => IsBishopMovement(chessPieceMoved, cp.Position.ColumnPosition, cp.Position.RowPosition));

            //Check for direction
            //Check for vertical movement
            if (chessPieceMoved.Position.RowPosition < finalRowPosition)
                PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.RowPosition < cp.Position.RowPosition);
            else
                PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.RowPosition > cp.Position.RowPosition);

            //Check for horizontal movement
            if(chessPieceMoved.Position.ColumnPosition < finalColumnPosition)
                PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.ColumnPosition < cp.Position.ColumnPosition);
            else
                PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.ColumnPosition > cp.Position.ColumnPosition);
            //Check if any pieces remain
            if (PiecesInTheWay.ToList().Count < 1) return false;

            //Take first one
            var closestPieceComparer = new ClosestChessPieceComparer(chessPieceMoved);
            IChessPiece closestPieceInTheWay;
            closestPieceInTheWay = PiecesInTheWay.OrderBy(cp => cp, closestPieceComparer).First();

            var distanceBetweenDestinationAndClosets = closestPieceComparer.FloatCompare(new Bishop(0, finalColumnPosition, finalRowPosition), closestPieceInTheWay);

            //Check if it blocks the move to final destination or is too far to interrupt movement
            bool isPieceInTheWay = distanceBetweenDestinationAndClosets >= 0;

            //Check if final destination and closest piece are on the same tile and different colors (Main piece tries to attack) 
            bool isMainAttacking = distanceBetweenDestinationAndClosets == 0 &&
                                    closestPieceInTheWay.Color != chessPieceMoved.Color;
            //If there is no piece in the way or main piece attacks the other allow move
            if (!isPieceInTheWay || isMainAttacking) return false;
            return true;
        }

    }
}
