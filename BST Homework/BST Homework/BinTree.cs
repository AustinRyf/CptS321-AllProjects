using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST_Homework
{
    public abstract class BinTree<T>
    {
        public abstract void InsertPublic(T val); // Insert new item of type T
        public abstract bool ContainsPublic(T val); // Returns true if item is in tree
        public abstract void InOrderPublic(); // Print elements in tree inorder traversal
        public abstract void PreOrderPublic();
        public abstract void PostOrderPublic();
    }
}
