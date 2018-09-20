using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotepadApp_Homework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void LoadText(TextReader sr)
        {
            //Clears the text box so another option can print to it
            textBox1.Clear();
            //Prints through the end of the file or string
            textBox1.AppendText(sr.ReadToEnd());
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opens the load file dialog and filters so that only text files can be opened
            OpenFileDialog loadFromFile = new OpenFileDialog();
            loadFromFile.Filter = "Plain Text File (*.txt)|*.txt";
            loadFromFile.ShowDialog();
            //Prints the text from the file to the text box
            using (StreamReader loadStream = new StreamReader(loadFromFile.FileName))
            {
                LoadText(loadStream);
            }
        }

        private void load50FibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creates a new Fibonacci object to create the first 50 items of the fibonnaci sequence
            FibonacciTextReader first50 = new FibonacciTextReader(50);
            //Prints the contents of the object to the text box
            LoadText(first50);
        }

        private void load100FibonacciNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creates a new Fibonacci object to create the first 100 items of the fibonnaci sequence
            FibonacciTextReader first100 = new FibonacciTextReader(100);
            //Prints the contents of the object to the text box
            LoadText(first100);
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opens the save file dialog and filters so that only text files can be saved to
            SaveFileDialog saveToFile = new SaveFileDialog();
            saveToFile.Filter = "Plain Text File (*.txt)|*.txt";
            saveToFile.ShowDialog();
            //Prints the text from the text box to the file
            using (StreamWriter saveStream = new StreamWriter(saveToFile.FileName))
            {
                saveStream.Write(textBox1.Text);
            }
        }
    }
}
