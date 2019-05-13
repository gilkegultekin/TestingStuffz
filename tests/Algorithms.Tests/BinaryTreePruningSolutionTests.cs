using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class BinaryTreePruningSolutionTests
    {
        [Fact]
        public void Test1()
        {
            TreeNode root = new TreeNode(1);
            TreeNode rootRight = new TreeNode(0);
            root.right = rootRight;
            TreeNode rootRightLeft = new TreeNode(0);
            rootRight.left = rootRightLeft;
            TreeNode rootRightRight = new TreeNode(1);
            rootRight.right = rootRightRight;

            BinaryTreePruningSolution solution = new BinaryTreePruningSolution();
            TreeNode result = solution.Solve(root);

            result.Should().NotBeNull();
            result.left.Should().BeNull();
            result.right.Should().NotBeNull();
            result.right.left.Should().BeNull();
            result.right.right.Should().NotBeNull();
            result.right.right.left.Should().BeNull();
            result.right.right.right.Should().BeNull();
        }

        [Fact]
        public void Test2()
        {
            TreeNode root = new TreeNode(1);
            TreeNode rootLeft = new TreeNode(0);
            root.left = rootLeft;
            TreeNode rootRight = new TreeNode(1);
            root.right = rootRight;
            TreeNode rootLeftLeft = new TreeNode(0);
            rootLeft.left = rootLeftLeft;
            TreeNode rootLeftRight = new TreeNode(0);
            rootLeft.right = rootLeftRight;
            TreeNode rootRightLeft = new TreeNode(0);
            rootRight.left = rootRightLeft;
            TreeNode rootRightRight = new TreeNode(1);
            rootRight.right = rootRightRight;

            BinaryTreePruningSolution solution = new BinaryTreePruningSolution();
            TreeNode result = solution.Solve(root);

            result.Should().NotBeNull();
            result.left.Should().BeNull();
            result.right.Should().NotBeNull();
            result.right.left.Should().BeNull();
            result.right.right.Should().NotBeNull();
            result.right.right.left.Should().BeNull();
            result.right.right.right.Should().BeNull();
        }
    }
}