using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    //Given an array of integers, return indices of the two numbers such that they add up to a specific target.
    //You may assume that each input would have exactly one solution, and you may not use the same element twice.
    //Example:
    //Given nums = [2, 7, 11, 15], target = 9,
    //Because nums[0] + nums[1] = 2 + 7 = 9,
    //return [0, 1].
    class TwoSumSolution : ISolution<int[], int[], int>
    {
        public int[] Solve(int[] nums, int target)
        {
            return BruteForce(nums, target);
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
    }
}
