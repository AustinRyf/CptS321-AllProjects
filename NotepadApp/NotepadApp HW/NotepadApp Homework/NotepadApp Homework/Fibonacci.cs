using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NotepadApp_Homework 
{
    class FibonacciTextReader : System.IO.TextReader
    {
        int totalLines = 0;
        int currentLine = 1;
        //Takes the number of lines as a parameter and sets that to the total lines for the overriding of read to end
        public FibonacciTextReader(int numLines)
        {
            totalLines = numLines;
        }

        //Found Fibonacci algorithm from https://www.dotnetperls.com/fibonacci
        public BigInteger FibonnaciNumberGenerator(int currentLine)
        {
            //These are big integers because normal integers can't hold the size of the later numbers in the sequence
            BigInteger a = 0;
            BigInteger b = 1;
            BigInteger temp = 0;
            //Needed for the fibonnaci sequence to start correctly
            if (currentLine == 1)
            {
                return a;
            }
            //The fibonacci algorithm
            for (int i = 1; i < currentLine; i++)
            {
                temp = a;
                a = b;
                b += temp;
            }

            return a;
        }
        
        public override string ReadLine()
        {
            return FibonnaciNumberGenerator(currentLine).ToString();
        }
        //In combination with the overriden RealLine(), this correctly prints the fibonnaci sequence
        public override string ReadToEnd()
        {
            string currentNumber = "";

            while(currentLine <= totalLines)
            {
                currentNumber = currentNumber + (currentLine + ": " + ReadLine() + "\r\n");
                currentLine++;
            }

            return currentNumber;
        }
    }
}
