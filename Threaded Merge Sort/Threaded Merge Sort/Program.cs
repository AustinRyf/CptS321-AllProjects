using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threaded_Merge_Sort
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Standard Merge Sort vs. Threaded Merge Sort Tests");
            Console.WriteLine("Array Sizes Under [8, 64, 256, 1024]");
            Console.WriteLine("----------------------------------------------------------");

            int[] list8 = makeRandomList(8);
            int[] list64 = makeRandomList(64);
            int[] list256 = makeRandomList(256);
            int[] list1024 = makeRandomList(1024);

            //foreach (int num in list8)
            //{
            //    Console.Write(num + " ");
            //}

            //foreach (int num in list64)
            //{
            //    Console.Write(num + " ");
            //}

            Console.Write("Starting test for size 8");
            runMerge(list8, 8);
            runThreadMerge(list8, 8);
            Console.WriteLine();

            Console.Write("Starting test for size 64");
            runMerge(list64, 64);
            runThreadMerge(list64, 64);
            Console.WriteLine();

            Console.Write("Starting test for size 256");
            runMerge(list256, 256);
            runThreadMerge(list256, 256);
            Console.WriteLine();

            Console.Write("Starting test for size 1024");
            runMerge(list1024, 1024);
            runThreadMerge(list1024, 1024);
            Console.WriteLine();
        }

        private static int[] makeRandomList(int size)
        {
            Random randomNum = new Random();

            int[] unsortedArr = new int[size];
            for (int i = 0; i < size; i++)
            {
                unsortedArr[i] = randomNum.Next(0, Int32.MaxValue);
            }

            return unsortedArr;
        }

        private static void runMerge(int[] arr, int size)
        {
            Stopwatch stopwatch = new Stopwatch();
            
            int[] regMergeList = (int[])arr.Clone();

            stopwatch.Start();
            RegMerge.MergeSort(ref regMergeList, 0, size - 1);
            stopwatch.Stop();

            TimeSpan timeTaken = stopwatch.Elapsed;

            //foreach (int num in regMergeList)
            //{
            //    Console.Write(num + " ");
            //}
            //Console.WriteLine();

            Console.WriteLine(" - Test completed:");
            Console.WriteLine("Normal Sort Time(ms): " + timeTaken);
        }

        private static void runThreadMerge(int[] arr, int size)
        {
            Stopwatch stopwatch = new Stopwatch();

            int[] threadMergeList = (int[])arr.Clone();

            //millisecondsStart = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            stopwatch.Start();
            Thread thread8_1 = new Thread(delegate () { RegMerge.MergeSort(ref threadMergeList, 0, size / 2 - 1); });
            Thread thread8_2 = new Thread(delegate () { RegMerge.MergeSort(ref threadMergeList, size / 2, size - 1); });
            thread8_1.Start();
            thread8_2.Start();
            thread8_1.Join();
            thread8_2.Join();
            RegMerge.MergeLists(ref threadMergeList, 0, size / 2 - 1, size - 1);
            stopwatch.Stop();
            //millisecondsStop = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            TimeSpan timeTaken = stopwatch.Elapsed;

            //foreach (int num in threadMergeList)
            //{
            //    Console.Write(num + " ");
            //}
            //Console.WriteLine();

            Console.WriteLine("Threaded Sort Time(ms): " + timeTaken);
        }
    }
}
