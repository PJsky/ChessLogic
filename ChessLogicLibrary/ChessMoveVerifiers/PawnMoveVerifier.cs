using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public class PawnMoveVerifier : IChessMoveVerifier
    {
        public bool Verify(IChessPiece chessPieceMoved, int finalColumnPosition, int finalRowPosition, List<IChessPiece> chessPiecesList = null)
        {
            chessPiecesList = chessPiecesList ?? new List<IChessPiece>();
            if (!IsSingleForwadMove(chessPieceMoved, finalColumnPosition, finalRowPosition, chessPiecesList) &&
                !IsAttackingDiagonally(chessPieceMoved, finalColumnPosition, finalRowPosition, chessPiecesList))
                return false;
            return true;

        }

        private bool IsSingleForwadMove(IChessPiece chessPieceMoved,
                                    int finalColumnPosition, int finalRowPosition,
                                    List<IChessPiece> chessPiecesList)
        {
            //Check for vertical movement as it is the only non attacking way of moving for pawn
            int verticalMovement = finalRowPosition - chessPieceMoved.Position.RowPosition;
            //Check for horizontal movement to assure it moves only vertically 
            int horizontalMovement = finalColumnPosition - chessPieceMoved.Position.ColumnPosition;
            //Check if it is a move for a proper pawn
            bool isWhitePawnMove = (verticalMovement == 1 || (verticalMovement == 2 && chessPieceMoved.wasMoved == false)) && horizontalMovement == 0 && chessPieceMoved.Color == ColorsEnum.White;
            bool isBlackPawnMove = (verticalMovement == -1 || (verticalMovement == -2 && chessPieceMoved.wasMoved == false)) && chessPieceMoved.Color == ColorsEnum.Black;

            if (!isWhitePawnMove && !isBlackPawnMove) return false;

            List<IChessPiece> chessPiecesInTheWay = new List<IChessPiece>();
            if (chessPiecesList.Count >= 1)
            { 
                //If single move check if something blocks destination
                if(Math.Abs(verticalMovement) == 1)
                    chessPiecesInTheWay = chessPiecesList.Where(cp => cp.Position.ColumnPosition == finalColumnPosition && cp.Position.RowPosition == finalRowPosition).ToList();

                //If double move check if somthing blocks destination or midway point
                else if(Math.Abs(verticalMovement) == 2)
                    chessPiecesInTheWay = chessPiecesList.Where(cp => 
                    (cp.Position.RowPosition == finalRowPosition || cp.Position.RowPosition == (finalRowPosition + chessPieceMoved.Position.RowPosition) /2) 
                    && cp.Position.ColumnPosition == finalColumnPosition
                    ).ToList();

                if (chessPiecesInTheWay.Count >= 1) return false;
            }
            return true;
        }

        private bool IsAttackingDiagonally(IChessPiece chessPieceMoved,
                                    int finalColumnPosition, int finalRowPosition,
                                    List<IChessPiece> chessPiecesList)
        {
            //Check for vertical movement as it is the only non attacking way of moving for pawn
            int verticalMovement = finalRowPosition - chessPieceMoved.Position.RowPosition;
            //Check for horizontal movement
            int horizontalMovement = finalColumnPosition - chessPieceMoved.Position.ColumnPosition;
            //Check if it is a attack pattern for a properly colored pawn
            bool isWhitePawnAttack = verticalMovement == 1 && Math.Abs(horizontalMovement) == 1 && chessPieceMoved.Color == ColorsEnum.White;
            bool isBlackPawnAttack = verticalMovement == -1 && Math.Abs(horizontalMovement) == 1 && chessPieceMoved.Color == ColorsEnum.Black;

            if (!isWhitePawnAttack && !isBlackPawnAttack) return false;

            if(chessPiecesList.Count >= 1)
            {
                var chessPiecesInTheWayWithDifferentColor = chessPiecesList.Where(cp => cp.Position.ColumnPosition == finalColumnPosition 
                && cp.Position.RowPosition == finalRowPosition
                && cp.Color != chessPieceMoved.Color).ToList();
                if (chessPiecesInTheWayWithDifferentColor.Count >= 1) return true;
            }
            return false;
        }
    }
}
