using Algorithms.BST;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class BinaryTreeMergeSolutionTests
    {
        [Fact]
        public void MergeSingleNodeTrees()
        {
            const int firstRootValue = 5;
            const int secondRootValue = 10;

            TreeNode firstRoot = new TreeNode(firstRootValue);
            TreeNode secondRoot = new TreeNode(secondRootValue);

            BinaryTreeMergeSolution solution = new BinaryTreeMergeSolution();
            TreeNode mergedRoot = solution.Solve(firstRoot, secondRoot);

            mergedRoot.Should().NotBeNull();
            mergedRoot.left.Should().BeNull();
            mergedRoot.right.Should().BeNull();
            mergedRoot.val.Should().Be(firstRootValue + secondRootValue);
        }

        [Fact]
        public void Merge_RootWithLeftChild_RootWithoutChildren()
        {
            const int firstRootValue = 5;
            const int secondRootValue = 10;
            const int leftChildValue = 2;

            TreeNode firstRoot = new TreeNode(firstRootValue);
            TreeNode secondRoot = new TreeNode(secondRootValue);
            TreeNode leftChild = new TreeNode(leftChildValue);
            firstRoot.left = leftChild;

            BinaryTreeMergeSolution solution = new BinaryTreeMergeSolution();
            TreeNode mergedRoot = solution.Solve(firstRoot, secondRoot);

            mergedRoot.Should().NotBeNull();
            mergedRoot.left.Should().NotBeNull();
            mergedRoot.left.val.Should().Be(leftChildValue);
            mergedRoot.left.left.Should().BeNull();
            mergedRoot.left.right.Should().BeNull();
            mergedRoot.right.Should().BeNull();
            mergedRoot.val.Should().Be(firstRootValue + secondRootValue);
        }

        [Fact]
        public void MergeCustomTrees()
        {
            //create left tree
            TreeNode leftTreeRoot = new TreeNode(1);
            TreeNode leftTreeNode1 = new TreeNode(3);
            leftTreeRoot.left = leftTreeNode1;
            TreeNode leftTreeNode2 = new TreeNode(2);
            leftTreeRoot.right = leftTreeNode2;
            TreeNode lefTreeNode3 = new TreeNode(5);
            leftTreeNode1.left = lefTreeNode3;

            //create right child
            TreeNode rightTreeRoot = new TreeNode(2);
            TreeNode rightTreeNode1 = new TreeNode(1);
            rightTreeRoot.left = rightTreeNode1;
            TreeNode rightTreeNode2 = new TreeNode(3);
            rightTreeRoot.right = rightTreeNode2;
            TreeNode rightTreeNode3 = new TreeNode(4);
            rightTreeNode1.right = rightTreeNode3;
            TreeNode rightTreeNode4 = new TreeNode(7);
            rightTreeNode2.right = rightTreeNode4;

            BinaryTreeMergeSolution solution = new BinaryTreeMergeSolution();
            TreeNode mergedRoot = solution.Solve(leftTreeRoot, rightTreeRoot);

            mergedRoot.Should().NotBeNull();
            mergedRoot.val.Should().Be(3);

            TreeNode mergedRootLeft = mergedRoot.left;
            mergedRootLeft.Should().NotBeNull();
            mergedRootLeft.val.Should().Be(4);

            TreeNode mergedRootRight = mergedRoot.right;
            mergedRootRight.Should().NotBeNull();
            mergedRootRight.val.Should().Be(5);

            TreeNode mergedRootLeftLeft = mergedRootLeft.left;
            mergedRootLeftLeft.Should().NotBeNull();
            mergedRootLeftLeft.val.Should().Be(5);
            mergedRootLeftLeft.left.Should().BeNull();
            mergedRootLeftLeft.right.Should().BeNull();

            TreeNode mergedRootLeftRight = mergedRootLeft.right;
            mergedRootLeftRight.Should().NotBeNull();
            mergedRootLeftRight.val.Should().Be(4);
            mergedRootLeftRight.left.Should().BeNull();
            mergedRootLeftRight.right.Should().BeNull();

            mergedRootRight.left.Should().BeNull();

            TreeNode mergedRootRightRight = mergedRootRight.right;
            mergedRootRightRight.Should().NotBeNull();
            mergedRootRightRight.val.Should().Be(7);
            mergedRootRightRight.left.Should().BeNull();
            mergedRootRightRight.right.Should().BeNull();
        }
    }
}