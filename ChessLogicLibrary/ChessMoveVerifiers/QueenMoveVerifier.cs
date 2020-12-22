using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessMoveVerifiers
{
    public class QueenMoveVerifier : IChessMoveVerifier
    {
        IChessMoveVerifier bishopMoveVerifier = new BishopMoveVerifier();
        IChessMoveVerifier rookMoveVerifier = new RookMoveVerifier();

        public bool Verify(IChessPiece chessPieceMoved, int finalColumnPosition, int finalRowPosition, List<IChessPiece> chessPiecesList = null)
        {
            if (bishopMoveVerifier.Verify(chessPieceMoved, finalColumnPosition, finalRowPosition, chessPiecesList) 
                || rookMoveVerifier.Verify(chessPieceMoved, finalColumnPosition, finalRowPosition, chessPiecesList))
                    return true;
            return false;
        }
    }
}
