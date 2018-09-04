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

    public void InsertNode(int nodeValue, ref BSTNode node)
    {
        if (node == null)
        {
            node = new BSTNode(nodeValue);
            count++;
        }

        else if (nodeValue == node.value)
        {
            return;
        }

        else if (nodeValue > node.value)
        {
            InsertNode(nodeValue, ref node.rightChild);
            tempDepth++;
        }

        else if (nodeValue < node.value)
        {
            InsertNode(nodeValue, ref node.leftChild);
            tempDepth++;
        }
    }

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


            stringArray = userInput.Split(' ');
            foreach (string i in stringArray)
            {
                numberList.Add(int.Parse(i));
            }

            foreach (int j in numberList)
            {
                InputTree.tempDepth = 1;

                InputTree.InsertNode(j, ref InputTree.root);

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