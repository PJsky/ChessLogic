using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.PreMoveConditions
{
    public interface IPreMoveCondition
    {
        bool Verify(string positionString, List<IChessPiece> chessPieces, ColorsEnum colorChecked);
    }
}
