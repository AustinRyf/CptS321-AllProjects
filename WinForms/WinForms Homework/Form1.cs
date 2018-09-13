using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Homework
{
    public partial class Form1 : Form
    {
        //Creates the list for the random numbers and the dictionary for problem 1
        private List<int> randomList = new List<int>() { };
        Dictionary<int, int> hashTable = new Dictionary<int, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Calls the function to populate the list with random numbers
            MakeRandomList();
            
            int uniqueNumbers1 = 0;
            int uniqueNumbers2 = 0;
            int uniqueNumbers3 = 0;

            uniqueNumbers1 = ProblemOne();

            textBox1.Text = "1. HashSet method: " + uniqueNumbers1 + " unique numbers\r\n";

            textBox1.Text += "The time complexity of the function \"ProblemOne\" is O(n) because it has to walk through the entire list of n values to populate the dictionary. " +
                "The functions that are called inside of \"ProblemOne\" have a time complexity of O(1) because they treat the dictionary like an array and go directly to an index " +
                "rather than treating it like a list and walking all the way through it.\r\n\r\n";

            uniqueNumbers2 = ProblemTwo();

            textBox1.Text += "2. O(1) storage method: " + uniqueNumbers2 + " unique numbers\r\n\r\n";

            uniqueNumbers3 = ProblemThree();

            textBox1.Text += "3. Sorted method: " + uniqueNumbers3 + " unique numbers";
        }

        //Populates the list with random integers
        private void MakeRandomList()
        {
            var randomNum = new Random();

            int i = 0;
            //Adds a random number to the list
            for (i = 0; i < 10000; i++)
            {
                randomList.Add(randomNum.Next(0, 20000));
            }
        }

        //Problem One
        private int ProblemOne()
        {
            int key = 0;

            for(int i = 0; i < 10000; i++)
            {
                //Assigns a key to the integer
                key = randomList[i].GetHashCode();

                //If the key is not already in the hash table this will add the key and the integer into the dictionary
                if (!hashTable.ContainsKey(key))
                {
                    hashTable.Add(key, randomList[i]);
                }
            }
            //Returns the number of elements in the dictionary
            return hashTable.Count();
        }

        //Problem Two
        private int ProblemTwo()
        {
            int cur = 0;
            int uniqueNumbers = 0;

            for(int i = 0; i < 10000; i++)
            {
                //Sets the current iteration integer to current 
                cur = randomList[i];

                //Iterates through the list and if the current integer is in the list it will subtract from the total number of unique numbers
                for(int j = i+1; j < 10000; j++)
                {
                    if(cur == randomList[j])
                    {
                        uniqueNumbers--;
                        break;
                    }
                }
            }

            //Counts the values in the list and since the repeat numbers have already been subtracted from the total numbers, it will return the total number of unique numbers
            foreach(int k in randomList)
            {
                uniqueNumbers++;
            }

            return uniqueNumbers;
        }

        //Problem Three
        private int ProblemThree()
        {
            int uniqueNumbers = 0;

            //Sorts the list of random numbers
            randomList.Sort();

            //Iterates through the sorted list and increases the number of unique numbers if the number next to the current iteration is not a duplicate
            for(int i = 0; i < 9999; i++)
            {
                if(randomList[i] != randomList[i+1])
                {
                    uniqueNumbers++;
                }
            }

            uniqueNumbers++;

            //Return the total number of unique numbers
            return uniqueNumbers;
        }
    }
}