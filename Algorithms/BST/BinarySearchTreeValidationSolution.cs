using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.BST
{
    /// <summary>
    /// 98. Validate Binary Search Tree
    /// Given a binary tree, determine if it is a valid binary search tree (BST).
    /// 
    /// Assume a BST is defined as follows:
    /// 
    /// The left subtree of a node contains only nodes with keys less than the node's key.
    /// The right subtree of a node contains only nodes with keys greater than the node's key.
    /// Both the left and right subtrees must also be binary search trees.
    /// 
    /// 
    /// Example 1:
    /// 
    /// 2
    /// / \
    /// 1   3
    /// 
    /// Input: [2,1,3]
    /// Output: true
    /// Example 2:
    /// 
    /// 5
    /// / \
    /// 1   4
    /// / \
    /// 3   6
    /// 
    /// Input: [5,1,4,null,null,3,6]
    /// Output: false
    /// Explanation: The root node's value is 5 but its right child's value is 4.
    /// </summary>
    public class BinarySearchTreeValidationSolution : ISolution<bool, TreeNode>
    {
        public bool IsValidBSTInOrderTraversal(TreeNode root)
        {
            return TraverseTreeInOrder(root, new List<int>());
        }

        private bool TraverseTreeInOrder(TreeNode node, List<int> explored)
        {
            if (node == null)
            {
                return true;
            }

            bool isLeftSubtreeValid = TraverseTreeInOrder(node.left, explored);
            if (!isLeftSubtreeValid)
            {
                return false;
            }

            if (explored.Any() && node.val <= explored.Last())
            {
                return false;
            }

            explored.Add(node.val);

            return TraverseTreeInOrder(node.right, explored);
        }

        public bool IsValidBSTRecursion(TreeNode root)
        {
            return root == null || IsValidBST(root, null, null);
        }

        private bool IsValidBST(TreeNode node, int? minVal, int? maxVal)
        {
            if (node == null)
            {
                return true;
            }

            if ((minVal.HasValue && node.val <= minVal) || (maxVal.HasValue && node.val >= maxVal.Value))
            {
                return false;
            }

            return IsValidBST(node.left, minVal, node.val) && IsValidBST(node.right, node.val, maxVal);
        }

        public bool Solve(TreeNode param)
        {
            return IsValidBSTInOrderTraversal(param);
        }
    }
}