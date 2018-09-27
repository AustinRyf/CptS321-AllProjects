using System;
using System.Collections.Generic;

namespace BST_Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            string[] stringArray;
            List<int> numberList = new List<int>() { };

            BinarySearchTree<int> InputTree = new BinarySearchTree<int> { };

            Console.Write("Enter a collection of numbers in the range [0, 100], seperated by spaces: ");

            userInput = Console.ReadLine();

            //Will split the user enetered string into an array of strings
            stringArray = userInput.Split(' ');
            //The loop will iterate for every string in the array
            foreach (string i in stringArray)
            {
                //Will convert the character into an integer and enter it into an array of numbers
                numberList.Add(int.Parse(i));
            }
            //The loop will iterate for every integer in the array
            foreach (int j in numberList)
            {
                InputTree.tempDepth = 1;
                //Inserts the current number into the tree as a node
                InputTree.InsertPublic(j);
                //Depth counter
                if (InputTree.tempDepth > InputTree.depth)
                {
                    InputTree.depth = InputTree.tempDepth;
                }
            }

            Console.Write("InOrder Tree Contents: ");
            InputTree.InOrderPublic();
            Console.WriteLine("");

            Console.Write("PreOrder Tree Contents: ");
            InputTree.PreOrderPublic();
            Console.WriteLine("");

            Console.Write("PostOrder Tree Contents: ");
            InputTree.PostOrderPublic();
            Console.WriteLine("");

            Console.WriteLine("Number of items: " + InputTree.count);
            Console.WriteLine("Number of levels: " + InputTree.depth);

            double minLevels = Math.Ceiling(Math.Log((double)InputTree.count + 1, 2) - 1);
            Console.Write("Minimum number of levels that a tree with " + InputTree.count + " nodes could have: " + (minLevels + 1) + "\n");
        }
    }
}