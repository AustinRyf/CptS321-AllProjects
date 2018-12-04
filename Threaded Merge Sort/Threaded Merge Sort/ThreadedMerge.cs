using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threaded_Merge_Sort
{
    class ThreadedMerge
    {
        static int currNum = 0;

        public static void threadMerge(ref int[] unsortedArr, int size)
        {
            int threadNum = currNum++;
            int left = threadNum * (size / 2);
            int right = (threadNum + 1) * (size / 2) - 1;
            int middle = left + (right - left) / 2;

            if (left < right)
            {
                RegMerge.MergeSort(ref unsortedArr, left, middle);
                RegMerge.MergeSort(ref unsortedArr, middle + 1, right);
                RegMerge.MergeLists(ref unsortedArr, left, middle, right);
            }
        }
    }
}
