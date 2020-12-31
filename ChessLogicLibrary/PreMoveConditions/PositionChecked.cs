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
        public bool Verify(string positionString, List<IChessPiece> chessPieces, ColorsEnum colorChecked)
        {
            Position position = new Position(positionString);
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
    }
}
