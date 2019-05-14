using Algorithms.BST;
using FluentAssertions;
using Xunit;
using BSTTreeNode = Algorithms.BST.TreeNode;

namespace Algorithms.Tests
{
    public class KthSmallestElementSolutionTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        public void Test1(int k, int expectedResult)
        {
            BSTTreeNode root = new BSTTreeNode(3);
            BSTTreeNode rootLeft = new BSTTreeNode(1);
            root.left = rootLeft;
            BSTTreeNode rootRight = new BSTTreeNode(4);
            root.right = rootRight;
            BSTTreeNode rootLeftRight = new BSTTreeNode(2);
            rootLeft.right = rootLeftRight;

            KthSmallestElementSolution solution = new KthSmallestElementSolution();
            int result = solution.Solve(root, k);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        [InlineData(6, 6)]
        public void Test2(int k, int expectedResult)
        {
            BSTTreeNode root = new BSTTreeNode(5);
            BSTTreeNode rootLeft = new BSTTreeNode(3);
            root.left = rootLeft;
            BSTTreeNode rootRight = new BSTTreeNode(6);
            root.right = rootRight;
            BSTTreeNode rootLeftLeft = new BSTTreeNode(2);
            rootLeft.left = rootLeftLeft;
            BSTTreeNode rootLeftRight = new BSTTreeNode(4);
            rootLeft.right = rootLeftRight;
            BSTTreeNode rootLeftLeftLeft = new BSTTreeNode(1);
            rootLeftLeft.left = rootLeftLeftLeft;

            KthSmallestElementSolution solution = new KthSmallestElementSolution();
            int result = solution.Solve(root, k);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(13, 12)]
        [InlineData(21, 20)]
        [InlineData(34, 33)]
        [InlineData(32, 31)]
        [InlineData(50, 49)]
        [InlineData(46, 45)]
        public void Test3(int k, int expectedResult)
        {
            int[] array =
            {
                45, 30, 46, 10, 36, -1, 49, 8, 24, 34, 42, 48, -1, 4, 9, 14, 25, 31, 35, 41, 43, 47, -1, 0, 6, -1, -1,
                11, 20, -1, 28, -1, 33, -1, -1, 37, -1, -1, 44, -1, -1, -1, 1, 5, 7, -1, 12, 19, 21, 26, 29, 32, -1, -1,
                38, -1, -1, -1, 3, -1, -1, -1, -1, -1, 13, 18, -1, -1, 22, -1, 27, -1, -1, -1, -1, -1, 39, 2, -1, -1,
                -1, 15, -1, -1, 23, -1, -1, -1, 40, -1, -1, -1, 16, -1, -1, -1, -1, -1, 17
            };

            BSTTreeNode root = BinarySearchTreeGenerator.GenerateFromArray(array);

            KthSmallestElementSolution solution = new KthSmallestElementSolution();
            int result = solution.Solve(root, k);

            result.Should().Be(expectedResult);
        }
    }
}