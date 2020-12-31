using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.PreMoveConditions
{
    public interface IPreMoveCondition
    {
        bool VerifyPosition(string positionString, List<IChessPiece> chessPieces, ColorsEnum colorChecked);
        bool VerifyPosition(Position position, List<IChessPiece> chessPieces, ColorsEnum colorChecked);
        bool VerifyKingPosition(IChessPiece pieceMoved, string finalPosition,
                                List<IChessPiece> chessPieces, ColorsEnum colorChecked);
    }
}
