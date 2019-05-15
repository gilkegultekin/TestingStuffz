using Algorithms.BST;
using FluentAssertions;
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
            //Assuming that the expectedResult array will be sorted
            for (int i = 0; i < result.Length; i++)
            {
                result[i].Should().Be(expectedResult[i]);
            }
        }
    }
}