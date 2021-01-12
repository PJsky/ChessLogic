using ChessLogicLibrary;
using ChessLogicLibrary.ChessBoardObjects;
using ChessLogicLibrary.ChessBoardSerializers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitChessTest.ChessBoardSerializerTests
{
    public class ChessBoardSerializerTests
    {

        [Fact]
        public void Serialize_StandardChessPieceSetup_ReturnsProperlySerialized()
        {
            IChessBoard chessBoard = new StandardChessBoard(new StandardChessPieceFactory());

            string serialized = StandardChessBoardSerializer.Serialize(chessBoard);

            Assert.Equal("RNBQKBNRPPPPPPPP00000000000000000000000000000000pppppppprnbqkbnr", serialized);
        }
    }
}
