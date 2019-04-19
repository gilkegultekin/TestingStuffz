﻿using System;
using System.Collections.Generic;

namespace Algorithms
{
    /// <summary>
    /// Find the kth largest element in an unsorted array. Note that it is the kth largest element in the sorted order, not the kth distinct element.
    /// 
    /// Example 1:
    /// 
    /// Input: [3,2,1,5,6,4] and k = 2
    /// Output: 5
    /// Example 2:
    /// 
    /// Input: [3,2,3,1,2,4,5,5,6] and k = 4
    /// Output: 4
    /// Note: 
    /// You may assume k is always valid, 1 ≤ k ≤ array's length.
    /// </summary>
    public class FindKthLargestElementSolution : ISolution<int, int[], int>
    {
        public int FindKthLargest(int[] nums, int k)
        {
            //Keep the largest k elements in a sorted list
            List<int> buffer = new List<int>(k+1);
            int smallestInBuffer = int.MinValue;

            foreach (int num in nums)
            {
                if (num > smallestInBuffer)
                {
                    smallestInBuffer = InsertIntoSortedList(buffer, num);
                }
            }

            return smallestInBuffer;
        }

        private int InsertIntoSortedList(List<int> buffer, int newNum)
        {
            int i = 0;
            while (i < buffer.Count && newNum < buffer[i])
            {
                i++;
            }

            buffer.Insert(i, newNum);

            if (buffer.Count == buffer.Capacity)
            {
                buffer.RemoveAt(buffer.Capacity - 1);
            }

            return buffer.Count == buffer.Capacity - 1 ? buffer[buffer.Count - 1] : int.MinValue;
        }

        public int Solve(int[] param1, int param2)
        {
            return FindKthLargest(param1, param2);
        }
    }
}