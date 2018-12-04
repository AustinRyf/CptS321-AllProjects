using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threaded_Merge_Sort
{
    class RegMerge //Algorithm found on https://www.geeksforgeeks.org/merge-sort/
    {
        public static void MergeSort(ref int[] unsortedArr, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                
                MergeSort(ref unsortedArr, left, middle);
                MergeSort(ref unsortedArr, middle + 1, right);

                MergeLists(ref unsortedArr, left, middle, right);
            }
        }

        public static void MergeLists(ref int[] unsortedArr, int left, int middle, int right)
        {
            int leftSize = middle - left + 1;
            int rightSize = right - middle;
            int i, j, k = 0;
            int[] Left = new int[leftSize];
            int[] Right = new int[rightSize];

            for (i = 0; i < leftSize; i++)
                Left[i] = unsortedArr[left + i];
            for (j = 0; j < rightSize; j++)
                Right[j] = unsortedArr[middle + 1 + j];
            
            i = 0; 
            j = 0;
            k = left;

            while (i < leftSize && j < rightSize)
            {
                if (Left[i] <= Right[j])
                {
                    unsortedArr[k] = Left[i];
                    i++;
                }

                else
                {
                    unsortedArr[k] = Right[j];
                    j++;
                }

                k++;
            }

            while (i < leftSize)
            {
                unsortedArr[k] = Left[i];
                i++;
                k++;
            }

            while (j < rightSize)
            {
                unsortedArr[k] = Right[j];
                j++;
                k++;
            }
        }

    }
}
