using System;
using System.Collections.Generic;

namespace Algorithms
{
    //Given an array of integers, return indices of the two numbers such that they add up to a specific target.
    //You may assume that each input would have exactly one solution, and you may not use the same element twice.
    //Example:
    //Given nums = [2, 7, 11, 15], target = 9,
    //Because nums[0] + nums[1] = 2 + 7 = 9,
    //return [0, 1].
    public class TwoSumSolution : ISolution<int[], int[], int>
    {
        public int[] Solve(int[] nums, int target)
        {
            return TwoPassHashTable(nums, target);
        }

        //double for loop
        private int[] BruteForce(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                var firstNum = nums[i];
                for (int j = 0; j < i; j++)
                {
                    var secondNum = nums[j];
                    if (firstNum + secondNum == target)
                    {
                        return new[] { j, i };
                    }
                }
            }

            return null;
        }

        private int[] OnePassHashTable(int[] nums, int target)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                var numToFind = target - nums[i];
                
                if (dict.ContainsKey(numToFind))
                {
                    return new[] { dict[numToFind], i };
                }

                if (!dict.ContainsKey(nums[i]))
                {
                    dict[nums[i]] = i;
                }
            }

            return null;
        }

        private int[] OnePassHashTable2(int[] nums, int target)
        {
            var dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                var numToFind = target - nums[i];

                if (dict.ContainsKey(numToFind))
                {
                    return new[] { dict[numToFind][0], i };
                }

                if (dict.ContainsKey(nums[i]))
                {
                    dict[nums[i]].Add(i);
                }
                else
                {
                    dict[nums[i]] = new List<int> { i };
                }
            }

            return null;
        }

        private int[] TwoPassHashTable(int[] nums, int target)
        {
            var dict = new Dictionary<int, List<int>>();
            for (var i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i]))
                {
                    dict[nums[i]].Add(i);
                }
                else
                {
                    dict[nums[i]] = new List<int> { i };
                }
            }

            for (var i = 0; i < nums.Length; i++)
            {
                var numToFind = target - nums[i];
                if (numToFind == nums[i])
                {
                    var lst = dict[numToFind];
                    if (lst.Count == 1)
                        continue;
                    return new[] {i, lst[1]};
                }

                if (dict.ContainsKey(numToFind))
                {
                    return new[] { i, dict[numToFind][0] };
                }
            }

            return null;
        }

        private int[] CopyIntegerArray(int[] nums)
        {
            var copy = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                copy[i] = nums[i];
            }

            return copy;
        }

        private int FindNumberInArray(int[] nums, int numToFind)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == numToFind)
                    return i;
            }

            return -1;
        }

        //Assuming a solution exists
        //Does not work for [3,3],6, assumes distinct numbers
        private int[] SortAndBinarySearch(int[] nums, int target)
        {
            var copy = CopyIntegerArray(nums);

            Array.Sort(copy);

            foreach (var currNum in copy)
            {
                var numToFind = target - currNum;
                //if (numToFind == currNum)
                //{

                //}

                var bsResult = BinarySearch(copy, numToFind);
                if (bsResult < 0) continue;

                var firstIndex = FindNumberInArray(nums, currNum);
                var secondIndex = FindNumberInArray(nums, numToFind);
                return firstIndex < secondIndex ? new[] { firstIndex, secondIndex } : new[] { secondIndex, firstIndex };
            }

            return null;
        }

        private int BinarySearch(int[] nums, int numToFind)
        {
            return BinarySearchImpl(nums, numToFind, 0, nums.Length - 1);
        }

        private int BinarySearchImpl(int[] nums, int numToFind, int startingIndex, int endIndex)
        {
            if (startingIndex == endIndex)
            {
                return nums[startingIndex] == numToFind ? startingIndex : -1;
            }

            var midIndex = (startingIndex + endIndex) / 2;
            if (nums[midIndex] == numToFind)
            {
                return midIndex;
            }

            if (nums[midIndex] > numToFind)
            {
                var newEndIndex = midIndex - 1;
                if (newEndIndex < startingIndex)
                    return -1;

                return BinarySearchImpl(nums, numToFind, startingIndex, newEndIndex);
            }

            var newStartingIndex = midIndex + 1;
            if (newStartingIndex > endIndex)
                return -1;

            return BinarySearchImpl(nums, numToFind, newStartingIndex, endIndex);
        }
    }
}
