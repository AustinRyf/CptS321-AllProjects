using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CptS321
{
    public class Spreadsheet
    {
        private int columnCount;
        private int rowCount;
        private int column;
        private int row;
        private Cell[,] createdSpreadsheet;
        private int i = 0, j = 0;

        public int ColumnCount { get { return columnCount; } }
        public int RowCount { get { return rowCount; } }
        public event PropertyChangedEventHandler CPropertyChanged;

        //Spreadsheet Constructor
        public Spreadsheet(int rowNum, int columnNum)
        {
            rowCount = rowNum;
            columnCount = columnNum;

            createdSpreadsheet = new Cell[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    createdSpreadsheet[row, column] = new DefaultCell(row, column);
                    createdSpreadsheet[row, column].PropertyChanged += SSPropertyChanged;
                }
            }
        }

        //Returns the cell at the given column and row (if it is in the spreadsheet)
        public Cell GetCell(int rowIndex, int columnIndex)
        {
            if (rowIndex > rowCount || columnIndex > columnCount)
            {
                return null;
            }

            else
            {
                return createdSpreadsheet[rowIndex, columnIndex];
            }
        }

        //If the value of the cell is an equation, the text will be presented as the evaluated equation
        public void SSPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Text")
            {
                if(((Cell)sender).Text.StartsWith("="))
                {
                    string userEq = ((Cell)sender).Text.Substring(1);

                    column = Convert.ToInt16(userEq[0]) - 'A';
                    row = Convert.ToInt16(userEq.Substring(1)) - 1;

                    ((Cell)sender).Value = (GetCell(row, column)).Value;
                }

                else
                {
                    ((Cell)sender).Value = ((Cell)sender).Text;
                }
            }

            CPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }

        //Demo that randomly sets the text of 50 random cells, sets cells B1 - B50 to "This is cell B #", and then sets each cell of column A equal to it's column B counterpart
        public void ButtonDemo()
        {
            Random random = new Random();

            while (i < 50)
            {
                int randomColumn = random.Next(0, 25);
                int randomRow = random.Next(0, 49);

                createdSpreadsheet[randomRow, randomColumn].Text = "This cell was selected randomly";

                i++;
            }

            for (j = 0; j < 50; j++)
            {
                this.createdSpreadsheet[j, 1].Text = "This is cell B" + (j + 1);
                this.createdSpreadsheet[j, 0].Text = "=B" + (j + 1);
            }
        }
    }
}
