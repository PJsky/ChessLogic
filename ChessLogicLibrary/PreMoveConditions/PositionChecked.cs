using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessLogicLibrary.PreMoveConditions
{
    public class PositionChecked : IPreMoveCondition
    {
        public bool VerifyPosition(string positionString, List<IChessPiece> chessPieces, ColorsEnum colorChecked)
        {
            Position position = new Position(positionString);
            return VerifyPosition(position, chessPieces, colorChecked);
        }

        public bool VerifyPosition(Position position, List<IChessPiece> chessPieces, ColorsEnum colorChecked)
        {
            bool IsPositionChecked = false;
            List<IChessPiece> enemyChessPieces = chessPieces.Where(cp => cp.Color != colorChecked).ToList();
            foreach (IChessPiece cp in enemyChessPieces)
            {
                if (cp.IsMovePossible(position.ColumnPosition, position.RowPosition, chessPieces))
                {
                    IsPositionChecked = true;
                    break;
                }
            }
            return IsPositionChecked;
        }

        public bool VerifyKingPosition(IChessPiece pieceMoved, string finalPositionString, 
                                       List<IChessPiece> chessPieces, ColorsEnum colorChecked)
        {
            Position finalPosition = new Position(finalPositionString);
            IChessPiece currentKing = chessPieces.Where(cp => cp.Name == "King" && cp.Color == colorChecked).FirstOrDefault();
            //Create a piece that will temporarily replace piece that is moving but at the final position
            IChessPiece dummyPiece = new Pawn((int)colorChecked, finalPositionString);
            if (currentKing == pieceMoved)
                dummyPiece = new King((int)colorChecked, finalPositionString);
            IChessPiece pieceTaken = chessPieces.Where(cp => cp.Position.ColumnPosition == finalPosition.ColumnPosition && cp.Position.RowPosition == finalPosition.RowPosition).FirstOrDefault();
            chessPieces.Remove(pieceMoved);
            //if(pieceTaken != null)
            chessPieces.Remove(pieceTaken);
            chessPieces.Add(dummyPiece);

            //Check if after the fake move king of the moving color is checked
            bool result = VerifyPosition(currentKing.Position, chessPieces, colorChecked);
            //If king is the moving piece we have to verify the new position
            if(dummyPiece.Name == "King") result = VerifyPosition(dummyPiece.Position, chessPieces, colorChecked);

            //Remove dummy and give back the pieces that were there at the beggining so that the reference type data won t be changed
            chessPieces.Remove(dummyPiece);
            if (pieceMoved != null)
                chessPieces.Add(pieceMoved);
            if (pieceTaken != null)
                chessPieces.Add(pieceTaken);

            return result;
        }

    }
}
