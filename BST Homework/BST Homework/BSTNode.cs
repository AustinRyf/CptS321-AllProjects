using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Homework
{
    class BSTNode<T> : IComparable<BSTNode<T>> where T: IComparable<T>
    {
        public T value;
        public BSTNode<T> leftChild = null;
        public BSTNode<T> rightChild = null;

        //Creates node when called and inserts a value
        public BSTNode(T num)
        {
            this.value = num;
        }

        public int CompareTo(BSTNode<T> node)
        {
            return value.CompareTo(node.value);
        }

        //Allows the == operator to compare nodes
        public static bool operator == (BSTNode<T> firstNode, BSTNode<T> secondNode)
        {
            if (System.Object.ReferenceEquals(firstNode, secondNode))
            {
                return true;
            }

            else if ((object)firstNode == null || (object)secondNode == null)
            {
                return false;
            }

            return firstNode.value.CompareTo(secondNode.value) == 0;
        }

        //Allows the != operator to compare nodes
        public static bool operator != (BSTNode<T> firstNode, BSTNode<T> secondNode)
        {
            if (System.Object.ReferenceEquals(firstNode, secondNode))
            {
                return false;
            }

            else if ((object)firstNode == null && (object)secondNode != null)
            {
                return true;
            }

            else if ((object)firstNode != null && (object)secondNode == null)
            {
                return true;
            }

            return firstNode.value.CompareTo(secondNode.value) != 0;
        }

        //Allows the >= operator to compare nodes
        public static bool operator >= (BSTNode<T> firstNode, BSTNode<T> secondNode)
        {
            if (firstNode == secondNode)
            {
                return true;
            }

            else if (firstNode > secondNode)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //Allows the <= operator to compare nodes
        public static bool operator <= (BSTNode<T> firstNode, BSTNode<T> secondNode)
        {
            if (firstNode == secondNode)
            {
                return true;
            }

            else if (firstNode < secondNode)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //Allows the > operator to compare nodes
        public static bool operator > (BSTNode<T> firstNode, BSTNode<T> secondNode)
        {
            if (System.Object.ReferenceEquals(firstNode, null))
            {
                return false;
            }

            else if (System.Object.ReferenceEquals(secondNode, null))
            {
                return false;
            }

            return firstNode.value.CompareTo(secondNode.value) > 0;
        }

        //Allows the < operator to compare nodes
        public static bool operator < (BSTNode<T> firstNode, BSTNode<T> secondNode)
        {
            if (System.Object.ReferenceEquals(firstNode, null))
            {
                return false;
            }

            else if (System.Object.ReferenceEquals(secondNode, null))
            {
                return false;
            }

            return firstNode.value.CompareTo(secondNode.value) < 0;
        }
    }
}
