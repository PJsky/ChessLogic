using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessPiecePosition;
using ChessLogicLibrary.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessBoardSerializers
{
    public static class StandardChessBoardSerializer
    {
        public static string Serialize(IChessBoard chessBoard)
        {
            var chessPieces = chessBoard.ChessPiecesOnBoard;
            StringBuilder piecesSerialized = new StringBuilder();
            for(int i=1; i<=8; i++)
            {
                for(int j=1; j <= 8; j++)
                {
                    string currentTileRepresentation = "0";
                    Position currentPosition = new Position(j,i);
                    string currentPositionString = currentPosition.ToString();
                    IChessPiece currentCp = chessBoard.GetAPieceFromPosition(currentPositionString);
                    if(currentCp == null)
                    {
                        piecesSerialized.Append(currentTileRepresentation);
                        continue;
                    }
                    switch (currentCp.Name)
                    {
                        case "Pawn":
                            currentTileRepresentation = "P";
                            break;
                        case "Rook":
                            currentTileRepresentation = "R";
                            break;
                        case "Knight":
                            currentTileRepresentation = "N";
                            break;
                        case "Bishop":
                            currentTileRepresentation = "B";
                            break;
                        case "Queen":
                            currentTileRepresentation = "Q";
                            break;
                        case "King":
                            currentTileRepresentation = "K";
                            break;
                    }
                    if (currentCp.Color == ColorsEnum.Black) currentTileRepresentation = currentTileRepresentation.ToLower();
                    piecesSerialized.Append(currentTileRepresentation);
                }
            }
            return piecesSerialized.ToString();
        }
    }
}
