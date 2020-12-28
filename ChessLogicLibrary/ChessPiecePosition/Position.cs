using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
        public Position(string position)
        {
            //Seperate string into numbers and characters using regular expressions
            var match = Regex.Match(position, @"([|A-Z|a-z| ]*)([\d]*)");
            var columnPositionAsString = match.Groups[1].Value.ToUpper();

            //If there is only one character change it into integer
            if (columnPositionAsString.Length != 1) throw new Exception("A column position string can only be 1 character long");
            int columnPosition = columnPositionAsString.ToUpper()[0] - 64;
            var rowPosition = int.Parse(match.Groups[2].Value);

            ChangePosition(columnPosition, rowPosition);
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
