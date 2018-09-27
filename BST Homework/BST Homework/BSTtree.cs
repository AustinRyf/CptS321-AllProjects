using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Homework
{
    class BinarySearchTree<T> : BinTree<T>
        where T : IComparable<T>
    {
        private BSTNode<T> tempNode;
        private BSTNode<T> newNode;
        private BSTNode<T> root;
        public int count;
        public int depth = 1;
        public int tempDepth = 1;

        //Creates tree when called and sets a root so inserting can begin
        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }

        public override void InsertPublic(T val)
        {
            newNode = new BSTNode<T>(val);

            if (root == null)
            {
                root = newNode;
                count++;
            }

            else
            {
                InsertPrivate(newNode, ref root);
            }
        }

        //Will insert a node into a tree based on the value of the node
        private void InsertPrivate(BSTNode<T> newNode, ref BSTNode<T> node)
        {
            //If there is no node in the current spot, place a new node there
            if (node == null)
            {
                node = newNode;
                count++;
            }
            //If there is already a node of the same value, return
            else if (newNode == node)
            {
                return;
            }
            //If the node is greater than the current node, move right
            else if (newNode > node)
            {
                InsertPrivate(newNode, ref node.rightChild);
                tempDepth++;
            }
            //If the node is less than the current node, move left
            else if (newNode < node)
            {
                InsertPrivate(newNode, ref node.leftChild);
                tempDepth++;
            }
        }

        public override bool ContainsPublic(T val)
        {
            tempNode = new BSTNode<T>(val);
            return ContainsPrivate(tempNode, ref root);
        }

        //Similarly to the insert function this traverses the tree based on the value we want to find
        private bool ContainsPrivate(BSTNode<T> tempNode, ref BSTNode<T> node)
        {
            if (node == null)
            {
                return false;
            }

            else if (tempNode < node)
            {
                ContainsPrivate(tempNode, ref node.leftChild);
            }

            else if (tempNode > node)
            {
                ContainsPrivate(tempNode, ref node.rightChild);
            }

            else if (tempNode == node)
            {
                return true;
            }

            return false;
        }

        public override void InOrderPublic()
        {
            InOrderPrivate(ref root);
        }

        //In Order Traversal and Print
        private void InOrderPrivate(ref BSTNode<T> node)
        {
            if (node != null)
            {
                InOrderPrivate(ref node.leftChild);
                Console.Write(node.value + " ");
                InOrderPrivate(ref node.rightChild);
            }
        }

        public override void PreOrderPublic()
        {
            PreOrderPrivate(ref root);
        }
        //Pre Order Traversal and Print
        private void PreOrderPrivate(ref BSTNode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.value + " ");
                PreOrderPrivate(ref node.leftChild);
                PreOrderPrivate(ref node.rightChild);
            }
        }
        //Post Order Traversal and Print
        public override void PostOrderPublic()
        {
            PostOrderPrivate(ref root);
        }

        private void PostOrderPrivate(ref BSTNode<T> node)
        {
            if (node != null)
            {
                PostOrderPrivate(ref node.leftChild);
                PostOrderPrivate(ref node.rightChild);
                Console.Write(node.value + " ");
            }
        }
    }
}
