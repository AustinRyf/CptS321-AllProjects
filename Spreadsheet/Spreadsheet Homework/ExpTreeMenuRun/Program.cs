using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;

namespace ExpTreeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            bool cont = true;

            //Create a default expression tree
            ExpTree userTree = new ExpTree("Hello*2*World");

            //While the user doesn't select quit
            while (cont)
            {
                Console.WriteLine("Menu(current expression = " + userTree.userExpression + ")");
                Console.WriteLine("1 = Enter a new expression");
                Console.WriteLine("2 = Set a variable value");
                Console.WriteLine("3 = Evaluate Tree");
                Console.WriteLine("4 = Quit");
                //Converts what the user types to an int for the switch statement
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        //Create a new expression tree
                        Console.Write("Enter a new expression: ");
                        userTree = new ExpTree(Console.ReadLine());
                        Console.WriteLine();
                        
                        cont = true;
                        break;

                    case 2:
                        Console.Write("Enter the variable's name: ");
                        //Gets a string for the variable dictionary
                        string variableName = Console.ReadLine();
                        Console.Write("Enter the variable's value: ");
                        //Gets a double value for the variable dictionary
                        double variableValue = Convert.ToDouble(Console.ReadLine());
                        //Creates the variable in the dictionary
                        userTree.SetVar(variableName, variableValue);
                        Console.WriteLine();

                        cont = true;
                        break;

                    case 3:
                        //Evaluates the expression tree
                        Console.WriteLine(userTree.Eval());
                        Console.WriteLine();

                        cont = true;
                        break;

                    case 4:
                        //End the program
                        Console.WriteLine("Done");

                        cont = false;
                        break;

                    default:
                        //Default if anything besides 1-4 is entered
                        Console.WriteLine("//This is a command the app will ignore");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
