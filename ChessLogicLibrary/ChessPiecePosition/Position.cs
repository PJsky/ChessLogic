using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLogicLibrary.ChessPiecePosition
{
    public class Position
    {
        /// <param name="columnPosition">Represents x coordinate</param>
        /// <param name="rowPosition">Represents y coordinate</param>
        public Position(int columnPosition, int rowPosition)
        {
            ChangePosition(columnPosition,rowPosition);
        }
        private int columnPosition;
        public int ColumnPosition { get { return columnPosition; } }
        private int rowPosition;
        public int RowPosition { get { return rowPosition; } }

        public void ChangePosition(int newColumnPosition, int newRowPosition)
        {
            columnPosition = newColumnPosition;
            rowPosition = newRowPosition;
        }
    }
}
