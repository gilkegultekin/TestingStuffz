using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class BinaryTreeConstructionSolutionTests
    {
        [Fact]
        public void Test1()
        {
            int[] preOrder = { 3, 9, 20, 15, 7 };
            int[] inOrder = { 9, 3, 15, 20, 7 };

            BinaryTreeConstructionSolution solution = new BinaryTreeConstructionSolution();
            TreeNode result = solution.Solve(preOrder, inOrder);

            result.val.Should().Be(3);
            result.left.val.Should().Be(9);
            result.left.left.Should().BeNull();
            result.left.right.Should().BeNull();
            result.right.val.Should().Be(20);
            result.right.left.val.Should().Be(15);
            result.right.right.val.Should().Be(7);
            result.right.left.left.Should().BeNull();
            result.right.left.right.Should().BeNull();
            result.right.right.left.Should().BeNull();
            result.right.right.right.Should().BeNull();
        }

        [Fact]
        public void Test2()
        {
            int[] preOrder = { 3, 20, 15, 7 };
            int[] inOrder = { 3, 15, 20, 7 };

            BinaryTreeConstructionSolution solution = new BinaryTreeConstructionSolution();
            TreeNode result = solution.Solve(preOrder, inOrder);

            result.val.Should().Be(3);
            result.left.Should().BeNull();
            result.right.val.Should().Be(20);
            result.right.left.val.Should().Be(15);
            result.right.right.val.Should().Be(7);
            result.right.left.left.Should().BeNull();
            result.right.left.right.Should().BeNull();
            result.right.right.left.Should().BeNull();
            result.right.right.right.Should().BeNull();
        }

        [Fact]
        public void Test3()
        {
            int[] preOrder = { 3, 20 };
            int[] inOrder = { 20, 3 };

            BinaryTreeConstructionSolution solution = new BinaryTreeConstructionSolution();
            TreeNode result = solution.Solve(preOrder, inOrder);

            result.val.Should().Be(3);
            result.left.val.Should().Be(20);
            result.left.left.Should().BeNull();
            result.left.right.Should().BeNull();
            result.right.Should().BeNull();
        }

        [Fact]
        public void Test4()
        {
            int[] preOrder = { 3, 20 };
            int[] inOrder = { 3, 20 };

            BinaryTreeConstructionSolution solution = new BinaryTreeConstructionSolution();
            TreeNode result = solution.Solve(preOrder, inOrder);

            result.val.Should().Be(3);
            result.right.val.Should().Be(20);
            result.right.left.Should().BeNull();
            result.right.right.Should().BeNull();
            result.left.Should().BeNull();
        }
    }
}