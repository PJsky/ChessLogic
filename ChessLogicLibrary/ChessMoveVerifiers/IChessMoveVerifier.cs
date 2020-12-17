using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public interface IChessMoveVerifier
    {
        bool Verify(IChessPiece chessPieceMoved,
                    int finalColumnPosition, int finalRowPosition,
                    List<IChessPiece> chessPiecesList);
    }
}
