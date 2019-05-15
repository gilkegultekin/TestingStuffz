using Algorithms.BST;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;
using BSTTreeNode = Algorithms.BST.TreeNode;

namespace Algorithms.Tests
{
    public class BinarySearchTreeValidationSolutionTests
    {
        [Theory]
        [InlineData(new[] { 2, 1, 3 }, true)]
        [InlineData(new[] { 5, 1, 4, -1, -1, 3, 6 }, false)]
        [InlineData(new[] { 2147483647 }, true)]
        [InlineData(new int[] { }, true)]
        [InlineData(new int[] { -2147483648, -1, 2147483647 }, true)]
        [InlineData(new[] { 5, 1, 6, -1, -1, 4, 7 }, false)]
        [MemberData(nameof(GetLargeTrees))]
        public void Test1(int[] bstArray, bool expectedResult)
        {
            BSTTreeNode root = BinarySearchTreeGenerator.GenerateFromArray(bstArray, false);

            BinarySearchTreeValidationSolution solution = new BinarySearchTreeValidationSolution();
            bool result = solution.Solve(root);

            result.Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> GetLargeTrees()
        {
            int[] validBstArray =
            {
                45, 30, 46, 10, 36, -1, 49, 8, 24, 34, 42, 48, -1, 4, 9, 14, 25, 31, 35, 41, 43, 47, -1, 0, 6, -1, -1,
                11, 20, -1, 28, -1, 33, -1, -1, 37, -1, -1, 44, -1, -1, -1, 1, 5, 7, -1, 12, 19, 21, 26, 29, 32, -1, -1,
                38, -1, -1, -1, 3, -1, -1, -1, -1, -1, 13, 18, -1, -1, 22, -1, 27, -1, -1, -1, -1, -1, 39, 2, -1, -1,
                -1, 15, -1, -1, 23, -1, -1, -1, 40, -1, -1, -1, 16, -1, -1, -1, -1, -1, 17
            };

            int[] invalidBstArray =
            {
                45, 30, 46, 10, 36, -1, 49, 8, 24, 34, 42, 48, -1, 4, 9, 14, 25, 31, 35, 41, 43, 47, -1, 0, 6, -1, -1,
                11, 20, -1, 28, -1, 33, -1, -1, 37, -1, -1, 44, -1, -1, -1, 1, 5, 7, -1, 12, 19, 21, 26, 29, 32, -1, -1,
                38, -1, -1, -1, 3, -1, -1, -1, -1, -1, -1, 18, -1, 13, 22, -1, 27, -1, -1, -1, -1, -1, 39, 2, -1,
                15, -1, -1, -1, -1, 23, -1, -1, -1, 40, -1, -1, -1, 16, -1, -1, -1, -1, -1, 17
            };

            List<object[]> data = new List<object[]> { new object[] { validBstArray, true }, new object[] { invalidBstArray, false } };
            return data;
        }
    }
}