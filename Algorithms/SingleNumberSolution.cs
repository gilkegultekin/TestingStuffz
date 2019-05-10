using System;

namespace Algorithms
{
    /// <summary>
    /// Given a non-empty array of integers, every element appears twice except for one. Find that single one.
    /// 
    /// Note:
    /// 
    /// Your algorithm should have a linear runtime complexity. Could you implement it without using extra memory?
    /// 
    /// Example 1:
    /// 
    /// Input: [2,2,1]
    /// Output: 1
    /// Example 2:
    /// 
    /// Input: [4,1,2,1,2]
    /// Output: 4
    /// </summary>
    public class SingleNumberSolution : ISolution<int, int[]>
    {
        public int Solve(int[] param)
        {
            return SingleNumber(param);
        }

        public int SingleNumber(int[] nums)
        {
            if (nums.Length == 1)
            {
                return nums[0];
            }

            //XOR everything

            int result = nums[0] ^ nums[1];
            for (int i = 2; i < nums.Length; i++)
            {
                result = result ^ nums[i];
            }

            return result;
        }
    }
}