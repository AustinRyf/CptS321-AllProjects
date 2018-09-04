using System;
using System.Collections.Generic;

class BSTNode
{
    public int value;
    public BSTNode leftChild;
    public BSTNode rightChild;

    //Creates node when called and inserts number into it
    public BSTNode(int userNum)
    {
        value = userNum;
        leftChild = null;
        rightChild = null;
    }
}

class BinarySearchTree
{
    public BSTNode root;
    public int count;
    public int depth = 1;
    public int tempDepth = 1;

    //Creates tree when called and sets a root so inserting can begin
    public BinarySearchTree()
    {
        root = null;
        count = 0;
    }

    //Will insert a node into a tree based on the value of the node
    public void InsertNode(int nodeValue, ref BSTNode node)
    {
        //If there is no node in the current spot, place a new node there
        if (node == null)
        {
            node = new BSTNode(nodeValue);
            count++;
        }
        //If there is already a node of the same value, return
        else if (nodeValue == node.value)
        {
            return;
        }
        //If the node is greater than the current node, move right
        else if (nodeValue > node.value)
        {
            InsertNode(nodeValue, ref node.rightChild);
            tempDepth++;
        }
        //If the node is less than the current node, move left
        else if (nodeValue < node.value)
        {
            InsertNode(nodeValue, ref node.leftChild);
            tempDepth++;
        }
    }

    //Traversal that will print out the list of nodes from least to greatest
    public void InOrder(ref BSTNode node)
    {
        if (node.leftChild != null)
        {
            InOrder(ref node.leftChild);
        }

        Console.Write(node.value + " ");

        if (node.rightChild != null)
        {
            InOrder(ref node.rightChild);
        }
    }
}

namespace Homework_Assignment_1___Cpts321
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            string[] stringArray;
            List<int> numberList = new List<int>() { };

            BinarySearchTree InputTree = new BinarySearchTree { };

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
                InputTree.InsertNode(j, ref InputTree.root);
                //Depth counter
                if (InputTree.tempDepth > InputTree.depth)
                {
                    InputTree.depth = InputTree.tempDepth;
                }
            }

            Console.Write("Tree Contents: ");
            InputTree.InOrder(ref InputTree.root);
            Console.WriteLine("");
            Console.WriteLine("Number of items: " + InputTree.count);
            Console.WriteLine("Number of levels: " + InputTree.depth);

            double minLevels = Math.Ceiling(Math.Log((double)InputTree.count + 1, 2) - 1);
            Console.Write("Minimum number of levels that a tree with " + InputTree.count + " nodes could have: " + (minLevels + 1) + "\n");
        }
    }
}