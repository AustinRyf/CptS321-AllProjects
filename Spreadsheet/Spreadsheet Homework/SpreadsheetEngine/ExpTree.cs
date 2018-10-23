using System;
using System.Collections.Generic;
using System.Text;

namespace CptS321
{
    public class ExpTree
    {
        private Node root;
        //Creates the variable dictionary
        public static Dictionary<string, double> variableDictionary = new Dictionary<string, double> { };
        public string userExpression;

        //Abstract node class that can be inherited by the different types of nodes
        private abstract class Node
        {
            public abstract double Eval();
        }
        
        public ExpTree(string expression)
        {
            //Cleares out any variables from previous expression trees
            variableDictionary.Clear();
            //Sets new expression
            this.userExpression = expression;
            //Creates the tree from the expression
            CreateExpTree(userExpression);
        }

        private Node CreateExpTree(string expression)
        {
            double valueNode;
            //Post Order walk through
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                //All of these work in Post Order
                if (expression[i] == '+')
                {
                    operationNode currentNodePlus = new operationNode(expression[i]);

                    if (root == null)
                    {
                        root = currentNodePlus;
                    }

                    currentNodePlus.leftChild = CreateExpTree(expression.Substring(0, i));
                    currentNodePlus.rightChild = CreateExpTree(expression.Substring(i + 1));

                    return currentNodePlus;
                }

                else if (expression[i] == '-')
                {
                    operationNode currentNodeMin = new operationNode(expression[i]);

                    if (root == null)
                    {
                        root = currentNodeMin;
                    }

                    currentNodeMin.leftChild = CreateExpTree(expression.Substring(0, i));
                    currentNodeMin.rightChild = CreateExpTree(expression.Substring(i + 1));

                    return currentNodeMin;
                }

                else if (expression[i] == '*')
                {
                    operationNode currentNodeMult = new operationNode(expression[i]);

                    if (root == null)
                    {
                        root = currentNodeMult;
                    }

                    currentNodeMult.leftChild = CreateExpTree(expression.Substring(0, i));
                    currentNodeMult.rightChild = CreateExpTree(expression.Substring(i + 1));

                    return currentNodeMult;
                }

                else if (expression[i] == '/')
                {
                    operationNode currentNodeDiv = new operationNode(expression[i]);

                    if (root == null)
                    {
                        root = currentNodeDiv;
                    }

                    currentNodeDiv.leftChild = CreateExpTree(expression.Substring(0, i));
                    currentNodeDiv.rightChild = CreateExpTree(expression.Substring(i + 1));

                    return currentNodeDiv;
                }
            }
            //If the parsing of the expression for a "valueNode", create and return valueNode
            if (double.TryParse(expression, out valueNode))
            {
                valueNode currentValueNode = new valueNode(valueNode);
                return currentValueNode;
            }
            //Otherwise create and return a variableNode
            else
            {
                variableNode currentVariableNode = new variableNode(expression);
                return currentVariableNode;
            }
        }

        //Operation node inheriting from Node
        private class operationNode : Node
        {
            //Only node class that should have children
            char operation;
            public Node leftChild;
            public Node rightChild;
            //Constructor
            public operationNode(char op)
            {
                operation = op;
                leftChild = null;
                rightChild = null;
            }
            //Eval override for operation type node
            public override double Eval()
            {
                switch(operation)
                {
                    //Adds/Subtracts/Multiplies/Divides the children of the operationNode
                    case '+':
                        return this.leftChild.Eval() + this.rightChild.Eval();

                    case '-':
                        return this.leftChild.Eval() - this.rightChild.Eval();

                    case '*':
                        return this.leftChild.Eval() * this.rightChild.Eval();

                    case '/':
                        return this.leftChild.Eval() / this.rightChild.Eval();

                    default:
                        return 0.0;
                }
            }
        }

        //Value node inheriting from Node
        private class valueNode : Node
        {
            double value;
            //Constructor
            public valueNode(double val)
            {
                value = val;
            }
            //Eval override for value type node
            public override double Eval()
            {
                return value;
            }
        }

        //Variable node inheriting from Node
        private class variableNode : Node
        {
            string varName;
            double varValue;
            //Constructor
            public variableNode(string variableName)
            {
                varName = variableName;
                varValue = 0.0;
            }
            //Eval ovveride for variable type node
            public override double Eval()
            {
                return varValue = variableDictionary[varName];
            }
        }

        //Takes in a string and a double and enters them into the variable dictionary if they aren't already in it
        public void SetVar(string varName, double varValue)
        {
            if (variableDictionary.ContainsKey(varName))
            {
                return;
            }

            else
            {
                variableDictionary[varName] = varValue;
            }
        }

        public double Eval()
        {
            return root.Eval();
        }
    }
}
