using System;
using System.Linq;

namespace Algorithms
{
    /// <summary>
    /// Given an array containing n distinct numbers taken from 0, 1, 2, ..., n, find the one that is missing from the array.
    /// 
    /// Example 1:
    /// 
    /// Input: [3,0,1]
    /// Output: 2
    /// Example 2:
    /// 
    /// Input: [9,6,4,2,3,5,7,0,1]
    /// Output: 8
    /// Note:
    /// Your algorithm should run in linear runtime complexity. Could you implement it using only constant extra space complexity?
    /// </summary>
    public class MissingNumberSolution : ISolution<int, int[]>
    {
        private readonly MissingNumberStrategy _strategy;

        public MissingNumberSolution(MissingNumberStrategy strategy)
        {
            _strategy = strategy;
        }

        public int MissingNumberWithSum1(int[] nums)
        {
            int largestNum = nums.Length;
            int sum = 0;
            bool zeroFlag = false;

            foreach (int num in nums)
            {
                if (num == 0)
                {
                    zeroFlag = true;
                    continue;
                }

                sum += num;
            }

            int supposedSum = (largestNum * (largestNum + 1)) / 2;
            int difference = supposedSum - sum;
            return difference == 0 ? (zeroFlag ? largestNum + 1 : 0) : difference;
        }

        public int MissingNumberWithSum2(int[] nums)
        {
            int largestNum = nums.Length;
            int difference = (largestNum * (largestNum + 1)) / 2;
            return nums.Aggregate(difference, (current, num) => current - num);
        }

        //Xor the values and the array indices. The missing number will stand out.
        public int MissingNumberWithXor(int[] nums)
        {
            int xor = 0;
            int i;
            for (i = 0; i < nums.Length; i++)
            {
                xor = xor ^ i ^ nums[i];
            }

            return xor ^ i;
        }

        public int Solve(int[] param)
        {
            switch (_strategy)
            {
                case MissingNumberStrategy.SumV1:
                    return MissingNumberWithSum1(param);
                case MissingNumberStrategy.SumV2:
                    return MissingNumberWithSum2(param);
                case MissingNumberStrategy.Xor:
                    return MissingNumberWithXor(param);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum MissingNumberStrategy
    {
        SumV1,
        SumV2,
        Xor
    }
}