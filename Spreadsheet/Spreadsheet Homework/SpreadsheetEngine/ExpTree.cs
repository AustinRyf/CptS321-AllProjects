using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            
            Stack<Node> treeStack = new Stack<Node>();
            Stack<string> operatorStack = new Stack<string>();
            Queue<string> outputQueue = new Queue<string>();

            var allTokens = new List<string>();
            //The characters that will be used to seperate the expression into tokens (-,+,/,*,(,),^)
            string seperateAt = @"([-/\+\*\(\)\^])";
            Regex regex = new Regex(seperateAt);

            //Splits the expression into tokens
            foreach (string token in regex.Split(expression))
            {
                allTokens.Add(token);
            }

            //Removes null space and whitespace
            for (int i = 0; i < allTokens.Count; i++)
            {
                if (allTokens[i] == "" || allTokens[i] == " ")
                {
                    allTokens.Remove(allTokens[i]);
                }
            }

            //Shunting Yard Algorithm (found algorithm @ https://brilliant.org/wiki/shunting-yard-algorithm/)
            foreach (string token in allTokens)
            {
                //For handling parenthesis, if the token is a ), everything in the operator stack will be put into the output queue until it reaches a (
                if (token == ")")
                {
                    while (operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Pop();
                }
                
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }

                //For handling different operator, checks order of operations precedence and will move from stack to queue as needed
                else if (token == "+" || token == "-" || token == "*" || token == "/" || token == "^")
                {
                    while (operatorStack.Count != 0 && OrderOfOperations(token) <= OrderOfOperations(operatorStack.Peek()))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Push(token);
                }

                //For handling variables and values
                else
                {
                    outputQueue.Enqueue(token);
                }
            }

            //Moves any operators left on the stack to the output queue
            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            //Builds the tree from the output queue
            foreach (string token in outputQueue)
            {
                //If the token is an operator, assign its children and push it to the tree stack
                if (token == "+" || token == "-" || token == "*" || token == "/" || token == "^")
                {
                    operationNode currentOpNode = new operationNode(token[0]);

                    currentOpNode.rightChild = treeStack.Pop();
                    currentOpNode.leftChild = treeStack.Pop();

                    treeStack.Push(currentOpNode);
                }

                //If the token is a value, parse it and push it to the tree stack
                else if (double.TryParse(token, out valueNode))
                {
                    valueNode currentValueNode = new valueNode(valueNode);

                    treeStack.Push(currentValueNode);
                }

                //If the token is a variable, push it to the tree stack
                else
                {
                    variableNode currentVariableNode = new variableNode(token);
                    treeStack.Push(currentVariableNode);
                }
            }
            //Assign the root to the top of the stack
            root = treeStack.Pop();
            return root;
        }

        //Order of operations precedence
        private int OrderOfOperations(string op)
        {
            if (op == "+")
            {
                return 2;
            }

            else if (op == "-")
            {
                return 2;
            }

            else if (op == "*")
            {
                return 3;
            }

            else if (op == "/")
            {
                return 3;
            }

            else if (op == "^")
            {
                return 4;
            }

            else
            {
                return -1;
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
                    //Adds, Subtracts, Multiplies, Divides, or Powers the children of the operationNode
                    case '+':
                        return this.leftChild.Eval() + this.rightChild.Eval();

                    case '-':
                        return this.leftChild.Eval() - this.rightChild.Eval();

                    case '*':
                        return this.leftChild.Eval() * this.rightChild.Eval();

                    case '/':
                        return this.leftChild.Eval() / this.rightChild.Eval();

                    case '^':
                        return Math.Pow(this.leftChild.Eval(),this.rightChild.Eval());

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
