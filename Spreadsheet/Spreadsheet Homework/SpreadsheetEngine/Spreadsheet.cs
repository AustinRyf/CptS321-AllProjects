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
        private Dictionary<Cell, List<Cell>> cellDependencies = new Dictionary<Cell, List<Cell>>();

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
                if (cellDependencies.ContainsKey((Cell)sender))
                {
                    cellDependencies[(Cell)sender].Clear();
                }

                cellDependencies.Remove((Cell)sender);

                evaluateCellDepTree((Cell)sender);
            }

            //If a cell changes and has other cells relying on it, this will change the effected cells
            else
            {
                cellsRelyingOn((Cell)sender);
            }

            CPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Value"));
        }
        
        public void evaluateCellDepTree(Cell currentCell)
        {
            //Builds an expression tree using one or more cells that rely on other cells to find the passed in cell's value
            if (currentCell.Text.StartsWith("="))
            {
                try
                {
                    string userEq = currentCell.Text.Substring(1);

                    ExpTree cellDepTree = new ExpTree(userEq);
                    List<string> cellVariables = cellDepTree.getVarNames();
                    List<Cell> cellDepList = new List<Cell>();

                    foreach (string variable in cellVariables)
                    {
                        column = Convert.ToInt16(variable[0]) - 'A';
                        row = Convert.ToInt16(variable.Substring(1)) - 1;
                        double variableValue = Convert.ToDouble(GetCell(row, column).Value);

                        cellDepTree.SetVar(variable, variableValue);

                        Cell currVariable = GetCell(row, column);

                        if (!cellDependencies.ContainsKey(currentCell))
                        {
                            cellDependencies.Add(currentCell, new List<Cell>());
                        }

                        cellDependencies[currentCell].Add(currVariable);
                    }

                    currentCell.Value = cellDepTree.Eval().ToString();
                }
                //Catches if a cell is referencing a non-existant cell
                catch
                {
                    currentCell.Value = "#REF!";
                    throw;
                }
            }
            //If the cell isn't a cell expression this just sets the value of the cell to the cell's text
            else
            {
                string userEq = currentCell.Text;
                ExpTree cellDepTree = new ExpTree(userEq);

                currentCell.Value = cellDepTree.Eval().ToString();
            }
        }

        //Adjusts cells that rely on changing cells
        private void cellsRelyingOn(Cell currentCell)
        {
            foreach (Cell parentCell in cellDependencies.Keys)
            {
                if (cellDependencies[parentCell].Contains(currentCell))
                {
                    evaluateCellDepTree(parentCell);
                }
            }
        }
        

        //Demo that randomly sets the text of 50 random cells, sets cells B1 - B50 to "This is cell B #", and then sets each cell of column A equal to it's column B counterpart
        //public void ButtonDemo()
        //{
        //    Random random = new Random();

        //    while (i < 50)
        //    {
        //        int randomColumn = random.Next(0, 25);
        //        int randomRow = random.Next(0, 49);

        //        createdSpreadsheet[randomRow, randomColumn].Text = "This cell was selected randomly";

        //        i++;
        //    }

        //    for (j = 0; j < 50; j++)
        //    {
        //        this.createdSpreadsheet[j, 1].Text = "This is cell B" + (j + 1);
        //        this.createdSpreadsheet[j, 0].Text = "=B" + (j + 1);
        //    }
        //}
    }
}
