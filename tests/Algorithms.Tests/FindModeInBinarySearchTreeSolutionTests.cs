using Algorithms.BST;
using FluentAssertions;
using System;
using Xunit;
using BSTTreeNode = Algorithms.BST.TreeNode;

namespace Algorithms.Tests
{
    public class FindModeInBinarySearchTreeSolutionTests
    {
        [Theory]
        [InlineData(new[] { 1, -1, 2, 2, }, new[] { 2 })]
        public void Test1(int[] bstArray, int[] expectedResult)
        {
            BSTTreeNode root = BinarySearchTreeGenerator.GenerateFromArray(bstArray, false);

            FindModeInBinarySearchTreeSolution solution = new FindModeInBinarySearchTreeSolution();
            int[] result = solution.Solve(root);

            result.Should().NotBeNull();
            result.Length.Should().Be(expectedResult.Length);
            Array.Sort(result);
            Array.Sort(expectedResult);
            for (int i = 0; i < result.Length; i++)
            {
                result[i].Should().Be(expectedResult[i]);
            }
        }
    }
}