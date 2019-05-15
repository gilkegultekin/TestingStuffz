using System.Collections.Generic;

namespace Algorithms.BST
{
    /// <summary>
    /// 501. Find Mode in Binary Search Tree
    /// Given a binary search tree (BST) with duplicates, find all the mode(s) (the most frequently occurred element) in the given BST.
    /// 
    /// Assume a BST is defined as follows:
    /// 
    /// The left subtree of a node contains only nodes with keys less than or equal to the node's key.
    /// The right subtree of a node contains only nodes with keys greater than or equal to the node's key.
    /// Both the left and right subtrees must also be binary search trees.
    /// 
    /// 
    /// For example:
    /// Given BST [1,null,2,2],
    /// 
    /// 1
    /// \
    /// 2
    /// /
    /// 2
    /// 
    /// 
    /// return [2].
    /// 
    /// Note: If a tree has more than one mode, you can return them in any order.
    /// 
    /// Follow up: Could you do that without using any extra space? (Assume that the implicit stack space incurred due to recursion does not count).
    /// </summary>
    public class FindModeInBinarySearchTreeSolution : ISolution<int[], TreeNode>
    {
        private readonly List<int> _modeValues = new List<int>();
        private int _modeCount;
        private int _currentValue = int.MinValue;
        private int _currentCount;

        public int[] FindMode(TreeNode root)
        {
            TraverseTree(root);
            return _modeValues.ToArray();
        }

        public void TraverseTree(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            TraverseTree(node.left);

            if (_currentValue == node.val)
            {
                _currentCount++;
                if (_currentCount > _modeCount)
                {
                    _modeCount = _currentCount;
                    _modeValues.Clear();
                    _modeValues.Add(_currentValue);
                }
                else if (_currentCount == _modeCount)
                {
                    _modeValues.Add(_currentValue);
                }
            }
            else
            {
                _currentCount = 1;
                _currentValue = node.val;
                if (_modeCount == 0)
                {
                    _modeCount = _currentCount;
                    _modeValues.Add(_currentValue);
                }
                else if (_currentCount == _modeCount)
                {
                    _modeValues.Add(_currentValue);
                }
            }

            TraverseTree(node.right);
        }

        public int[] Solve(TreeNode param)
        {
            return FindMode(param);
        }
    }
}