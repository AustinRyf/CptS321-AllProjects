// Austin Ryf - 11512650

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CptS321;
using System.IO;

namespace Spreadsheet_Homework
{
    public partial class Form1 : Form
    {
        //Creates spreadsheet with 50 rows and 26 columns
        private Spreadsheet spreadSheetView = new Spreadsheet(50, 26);
        private Cell clickedCell;

        public Form1()
        {
            InitializeComponent();
        }

        //When a cell's value is changed, the cell in the winforms spreadsheet is also changed
        private void SVPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;

            if (cell != null && e.PropertyName == "Value")
            {
                dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
            }
        }

        //Adds the value of the cell to the winforms app as a string
        private void EndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                spreadSheetView.GetCell(e.RowIndex, e.ColumnIndex).Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }

            else
            {
                spreadSheetView.GetCell(e.RowIndex, e.ColumnIndex).Text = "";
            }
        }

        //When the user leaves the text box the cell will be updated
        private void textBox1_Leave(object sender, EventArgs e)
        {
            dataGridView1.Rows[clickedCell.RowIndex].Cells[clickedCell.ColumnIndex].Value = textBox1.Text;
            EndEdit(dataGridView1, new DataGridViewCellEventArgs(clickedCell.ColumnIndex, clickedCell.RowIndex));
        }

        //When the user presses enter when using the text box, the cell will be updated
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                textBox1_Leave(sender, new EventArgs());
            }
        }

        //Needed for textBox1_KeyDown
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                e.Handled = false;
                e.SuppressKeyPress = false;
            }
        }

        //When a cell is clicked, the text box will update to show that cell's text
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            clickedCell = spreadSheetView.GetCell(e.RowIndex, e.ColumnIndex);
            textBox1.Text = clickedCell.Text;
        }

        //Creates the visual spreadsheet with columns A-Z and rows 1-50
        private void Form1_Load(object sender, EventArgs e)
        {
            spreadSheetView.CPropertyChanged += SVPropertyChanged;

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (char i = 'A'; i <= 'Z'; i++)
            {
                DataGridViewTextBoxColumn column;
                column = new DataGridViewTextBoxColumn();

                column.Name = i.ToString();

                dataGridView1.Columns.Add(column);
            }

            dataGridView1.Rows.Add(50);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }

            //User events
            dataGridView1.CellEnter += new DataGridViewCellEventHandler(dataGridView1_CellEnter);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(EndEdit);
            textBox1.Leave += textBox1_Leave;
            textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            textBox1.KeyUp += new KeyEventHandler(textBox1_KeyUp);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadFromFile = new OpenFileDialog();
            loadFromFile.Filter = "XML File (*.xml)|*.xml";
            
            if (loadFromFile.ShowDialog() == DialogResult.OK)
            {
                emptySpreadsheet();

                FileStream loadStream = new FileStream(loadFromFile.FileName, FileMode.Open, FileAccess.Read);

                spreadSheetView.SpreadsheetLoad(loadStream);

                loadStream.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream fileStream;
            SaveFileDialog saveToFile = new SaveFileDialog();
            saveToFile.Filter = "XML File (*.xml)|*.xml";

            if (saveToFile.ShowDialog() == DialogResult.OK)
            {
                fileStream = saveToFile.OpenFile();
                spreadSheetView.SpreadsheetSave(fileStream);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void emptySpreadsheet()
        {
            for (int row = 0; row < 50; ++row)
            {
                for (int col = 0; col < 26; ++col)
                {
                    spreadSheetView.GetCell(row, col).Text = "";
                }
            }
        }
    }
}
