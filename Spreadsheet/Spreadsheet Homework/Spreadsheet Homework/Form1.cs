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

namespace Spreadsheet_Homework
{
    public partial class Form1 : Form
    {
        //Creates spreadsheet with 50 rows and 26 columns
        private Spreadsheet spreadSheetView = new Spreadsheet(50, 26);

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
        }

        //Runs the demo when the button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            spreadSheetView.ButtonDemo();
        }
    }
}
