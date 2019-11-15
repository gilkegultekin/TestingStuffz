using System;

namespace Algorithms
{
    /// <summary>
    /// Counts inversions in a given 1-d array piggybacking on MergeSort, inspired by the Algorithms course on Coursera.org (offered by Stanford Uni.)
    /// </summary>
    public class CountingInversionsSolution : ISolution<int, int[]>
    {
        public int Solve(int[] array)
        {
            //dont bother processing empty or single element arrays
            if (array == null || array.Length <= 1)
            {
                return 0;
            }

            (_, int inversionCount) = CountInversionsAndSort(array);
            return inversionCount;
        }

        private (int[],int) CountInversionsAndSort(int[] array)
        {
            //base case
            if (array.Length == 1)
            {
                return (array, 0);
            }

            //divide array into two
            int mid = array.Length / 2;
            int[] left = array.AsMemory(0, mid).ToArray();
            int[] right = array.AsMemory(mid).ToArray();

            //count left inversions recursively
            (int[] sortedLeft, int leftInversionCount) = CountInversionsAndSort(left);

            //count right inversions recursively
            (int[] sortedRight, int rightInversionCount) = CountInversionsAndSort(right);

            //count split inversions while merging
            (int[] sorted, int splitInversionCount) = MergeAndCount(sortedLeft, sortedRight);

            //return total number of inversions and the sorted array
            return (sorted, leftInversionCount + rightInversionCount + splitInversionCount);
        }

        private (int[], int) MergeAndCount(int[] left, int[] right)
        {
            int leftCounter, rightCounter, inversionCount, sortedCounter;
            leftCounter = rightCounter = inversionCount = sortedCounter = 0;
            int[] sorted = new int[left.Length + right.Length];

            while (leftCounter < left.Length && rightCounter < right.Length)
            {
                if (left[leftCounter] > right[rightCounter])
                {
                    //interesting case, signifies that there are a number of inversions
                    sorted[sortedCounter++] = right[rightCounter++];
                    inversionCount += left.Length - leftCounter;
                }
                else
                {
                    //copy the the next element from left to the sorted array, no inversions
                    sorted[sortedCounter++] = left[leftCounter++];
                }
            }

            //one of the arrays should be empty, concatenate the other one to the end of the sorted array
            if (leftCounter < left.Length)
            {
                for (int i = leftCounter; i < left.Length; i++)
                {
                    sorted[sortedCounter++] = left[i];
                }
            }
            else
            {
                for (int i = rightCounter; i < right.Length; i++)
                {
                    sorted[sortedCounter++] = right[i];
                }
            }

            //return the sorted array and the number of inversions
            return (sorted, inversionCount);
        }
    }
}