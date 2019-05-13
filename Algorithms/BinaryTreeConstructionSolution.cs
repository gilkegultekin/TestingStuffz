using System.Collections.Generic;

namespace Algorithms
{
    /// <summary>
    /// 105. Construct Binary Tree from Preorder and Inorder Traversal
    /// Given preorder and inorder traversal of a tree, construct the binary tree.
    /// 
    /// Note:
    /// You may assume that duplicates do not exist in the tree.
    /// 
    /// For example, given
    /// 
    /// preorder = [3,9,20,15,7]
    /// inorder = [9,3,15,20,7]
    /// Return the following binary tree:
    /// 
    ///     3
    ///    / \
    ///    9  20
    ///      /  \
    ///     15   7
    /// </summary>
    public class BinaryTreeConstructionSolution : ISolution<TreeNode, int[], int[]>
    {
        public TreeNode BuildTreeBrute(int[] preorder, int[] inorder)
        {
            if (preorder.Length == 1)
            {
                return new TreeNode(preorder[0]);
            }

            //find the root in inorder array
            //everything before the root will be in the left subtree, everything after the root will be in the right subtree

            List<int> leftSubtreeInOrder = new List<int> { };
            List<int> rightSubtreeInOrder = new List<int> { };

            int currentElementPreOrder = preorder[0];

            int j = 0;
            while (j < inorder.Length && inorder[j] != currentElementPreOrder)
            {
                leftSubtreeInOrder.Add(inorder[j++]);
            }

            if (j < inorder.Length)
            {
                j++;
            }

            while (j < inorder.Length && j < inorder.Length)
            {
                rightSubtreeInOrder.Add(inorder[j]);
            }

            TreeNode root = new TreeNode(preorder[0]);
            ConstructSubTrees(root, leftSubtreeInOrder, rightSubtreeInOrder, preorder, 1);
            return root;
        }

        private TreeNode ConstructSubTrees(TreeNode node, List<int> leftSubtreeInOrder, List<int> rightSubtreeInOrder,
            int[] preOrder, int currentPreOrderIndex)
        {
            int currentElementPreOrder = preOrder[currentPreOrderIndex];
            List<int> leftSubtreeOfCurrentElement = new List<int>{ };
            List<int> rightSubtreeOfCurrentElement = new List<int> { };
            bool leftSubtreeContainsCurrentElement = false;
            bool rightSubtreeContainsCurrentElement = false;

            foreach (int elm in leftSubtreeInOrder)
            {
                if (!leftSubtreeContainsCurrentElement)
                {
                    if (elm != currentElementPreOrder)
                    {
                        leftSubtreeOfCurrentElement.Add(elm);
                    }
                    else
                    {
                        leftSubtreeContainsCurrentElement = true;
                    }
                }
                else
                {
                    rightSubtreeOfCurrentElement.Add(elm);
                }
            }

            if (!leftSubtreeContainsCurrentElement)
            {
                leftSubtreeOfCurrentElement.Clear();
            }

            foreach (int elm in rightSubtreeInOrder)
            {
                if (!rightSubtreeContainsCurrentElement)
                {
                    if (elm != currentElementPreOrder)
                    {
                        leftSubtreeOfCurrentElement.Add(elm);
                    }
                    else
                    {
                        rightSubtreeContainsCurrentElement = true;
                    }
                }
                else
                {
                    rightSubtreeOfCurrentElement.Add(elm);
                }
            }


        }

        public TreeNode Solve(int[] param1, int[] param2)
        {
            return BuildTreeBrute(param1, param2);
        }
    }
}