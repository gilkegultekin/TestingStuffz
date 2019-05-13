using Algorithms.BST;
using FluentAssertions;
using Xunit;
using BSTTreeNode = Algorithms.BST.TreeNode;

namespace Algorithms.Tests
{
    public class BinaryTreeMergeSolutionTests
    {
        [Fact]
        public void MergeSingleNodeTrees()
        {
            const int firstRootValue = 5;
            const int secondRootValue = 10;

            BSTTreeNode firstRoot = new BSTTreeNode(firstRootValue);
            BSTTreeNode secondRoot = new BSTTreeNode(secondRootValue);

            BinaryTreeMergeSolution solution = new BinaryTreeMergeSolution();
            BSTTreeNode mergedRoot = solution.Solve(firstRoot, secondRoot);

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

            BSTTreeNode firstRoot = new BSTTreeNode(firstRootValue);
            BSTTreeNode secondRoot = new BSTTreeNode(secondRootValue);
            BSTTreeNode leftChild = new BSTTreeNode(leftChildValue);
            firstRoot.left = leftChild;

            BinaryTreeMergeSolution solution = new BinaryTreeMergeSolution();
            BSTTreeNode mergedRoot = solution.Solve(firstRoot, secondRoot);

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
            BSTTreeNode leftTreeRoot = new BSTTreeNode(1);
            BSTTreeNode leftTreeNode1 = new BSTTreeNode(3);
            leftTreeRoot.left = leftTreeNode1;
            BSTTreeNode leftTreeNode2 = new BSTTreeNode(2);
            leftTreeRoot.right = leftTreeNode2;
            BSTTreeNode lefTreeNode3 = new BSTTreeNode(5);
            leftTreeNode1.left = lefTreeNode3;

            //create right child
            BSTTreeNode rightTreeRoot = new BSTTreeNode(2);
            BSTTreeNode rightTreeNode1 = new BSTTreeNode(1);
            rightTreeRoot.left = rightTreeNode1;
            BSTTreeNode rightTreeNode2 = new BSTTreeNode(3);
            rightTreeRoot.right = rightTreeNode2;
            BSTTreeNode rightTreeNode3 = new BSTTreeNode(4);
            rightTreeNode1.right = rightTreeNode3;
            BSTTreeNode rightTreeNode4 = new BSTTreeNode(7);
            rightTreeNode2.right = rightTreeNode4;

            BinaryTreeMergeSolution solution = new BinaryTreeMergeSolution();
            BSTTreeNode mergedRoot = solution.Solve(leftTreeRoot, rightTreeRoot);

            mergedRoot.Should().NotBeNull();
            mergedRoot.val.Should().Be(3);

            BSTTreeNode mergedRootLeft = mergedRoot.left;
            mergedRootLeft.Should().NotBeNull();
            mergedRootLeft.val.Should().Be(4);

            BSTTreeNode mergedRootRight = mergedRoot.right;
            mergedRootRight.Should().NotBeNull();
            mergedRootRight.val.Should().Be(5);

            BSTTreeNode mergedRootLeftLeft = mergedRootLeft.left;
            mergedRootLeftLeft.Should().NotBeNull();
            mergedRootLeftLeft.val.Should().Be(5);
            mergedRootLeftLeft.left.Should().BeNull();
            mergedRootLeftLeft.right.Should().BeNull();

            BSTTreeNode mergedRootLeftRight = mergedRootLeft.right;
            mergedRootLeftRight.Should().NotBeNull();
            mergedRootLeftRight.val.Should().Be(4);
            mergedRootLeftRight.left.Should().BeNull();
            mergedRootLeftRight.right.Should().BeNull();

            mergedRootRight.left.Should().BeNull();

            BSTTreeNode mergedRootRightRight = mergedRootRight.right;
            mergedRootRightRight.Should().NotBeNull();
            mergedRootRightRight.val.Should().Be(7);
            mergedRootRightRight.left.Should().BeNull();
            mergedRootRightRight.right.Should().BeNull();
        }
    }
}