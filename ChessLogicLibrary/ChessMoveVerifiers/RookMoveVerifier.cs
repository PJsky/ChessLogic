using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChessLogicLibrary.ChessMoveVerifiers.ChessPieceComparers;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public class RookMoveVerifier : IChessMoveVerifier
    {
        public bool Verify(IChessPiece chessPieceMoved,
                           int finalColumnPosition, int finalRowPosition,
                           List<IChessPiece> chessPiecesList = null)
        {
            chessPiecesList = chessPiecesList ?? new List<IChessPiece>();
            if (!IsRookMovement(chessPieceMoved, finalColumnPosition, finalRowPosition) ||
                IsOtherPieceBlockingMove(chessPieceMoved,finalColumnPosition,finalRowPosition,chessPiecesList)) 
                return false;
            return true;
            
        }

        private bool IsRookMovement(IChessPiece chessPieceMoved,
                                      int finalColumnPosition, int finalRowPosition)
        {
            //Check if it actually moves
            if (chessPieceMoved.Position.ColumnPosition == finalColumnPosition
                && chessPieceMoved.Position.RowPosition == finalRowPosition)
                return false;

            var hasMovedHorizontally = chessPieceMoved.Position.ColumnPosition != finalColumnPosition;
            var hasMovedVertically = chessPieceMoved.Position.RowPosition != finalRowPosition;

            //Check if piece moves only vertically or horizontaly
            return hasMovedHorizontally ^ hasMovedVertically;
        }

        private bool IsOtherPieceBlockingMove(IChessPiece chessPieceMoved,
                                        int finalColumnPosition, int finalRowPosition,
                                        List<IChessPiece> chessPiecesList)
        {
            //Check if there are other pieces on board
            if (chessPiecesList.Count < 1) return false;

            //Filter away all pieces that rook could not even touch
            var PiecesInTheWay = chessPiecesList.Where(cp => IsRookMovement(chessPieceMoved, cp.Position.ColumnPosition, cp.Position.RowPosition));

            //Check for direction of movement
            RookMovementDirection? direction = null;

            if(chessPieceMoved.Position.ColumnPosition < finalColumnPosition) direction = RookMovementDirection.Right;
            else if(chessPieceMoved.Position.ColumnPosition > finalColumnPosition)direction = RookMovementDirection.Left;
            else if(chessPieceMoved.Position.RowPosition < finalRowPosition) direction = RookMovementDirection.Up;
            else if (chessPieceMoved.Position.RowPosition > finalRowPosition) direction = RookMovementDirection.Down;

            switch (direction)
            {
                case RookMovementDirection.Up:
                    PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.RowPosition < cp.Position.RowPosition);
                    break;
                case RookMovementDirection.Right:
                    PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.ColumnPosition < cp.Position.ColumnPosition);
                    break;
                case RookMovementDirection.Down:
                    PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.RowPosition > cp.Position.RowPosition);
                    break;
                case RookMovementDirection.Left:
                    PiecesInTheWay = PiecesInTheWay.Where(cp => chessPieceMoved.Position.ColumnPosition > cp.Position.ColumnPosition);
                    break;
            }

            if (PiecesInTheWay.ToList().Count < 1) return false;

            var closestPieceComparer = new ClosestChessPieceComparer(chessPieceMoved);
            IChessPiece closestPieceInTheWay = PiecesInTheWay.OrderBy(cp => cp, closestPieceComparer).First();

            var distanceBetweenDestinationAndClosets = closestPieceComparer.FloatCompare(new Rook(0, finalColumnPosition, finalRowPosition), closestPieceInTheWay);

            //Check if it blocks the move to final destination or is too far to interrupt movement
            bool isPieceInTheWay = distanceBetweenDestinationAndClosets >= 0;

            //Check if final destination and closest piece are on the same tile and different colors (Main piece tries to attack) 
            bool isMainAttacking = distanceBetweenDestinationAndClosets == 0 &&
                                    closestPieceInTheWay.Color != chessPieceMoved.Color;
            //If there is no piece in the way or main piece attacks the other allow move
            if (!isPieceInTheWay || isMainAttacking) return false;
            return true;
        }

        private enum RookMovementDirection : int
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

    }
}
