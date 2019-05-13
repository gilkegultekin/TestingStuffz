using System.Collections.Generic;
using System.Linq;

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
        //find the root in inorder array
        //everything before the root will be in the left subtree, everything after the root will be in the right subtree

        //the second node in pre order traversal has to be the root's child, but we have to check if its the left child or the right (by checking in which subtree it resides)
        //the third node in pre order traversal is the right child of the root if the previous node is left child and the third node resides in the right subtree

        //find the root in in-order traversal. Everything before it will be in the left subtree and everything after it will be in the right subtree
        public TreeNode BuildTreeBrute(int[] preorder, int[] inorder)
        {
            return ConstructSubtree(preorder.ToList(), inorder.ToList());
        }

        private TreeNode ConstructSubtree(List<int> preOrder, List<int> inOrder)
        {
            if (preOrder.Count == 0)
            {
                return null;
            }

            int rootValue = preOrder[0];
            TreeNode root = new TreeNode(rootValue);

            if (preOrder.Count == 1)
            {
                return root;
            }

            List<int> leftInOrder = new List<int>();
            List<int> rightInOrder = new List<int>();
            bool rootEncountered = false;
            Dictionary<int, bool> lookupTable = new Dictionary<int, bool>();

            foreach (int element in inOrder)
            {
                if (element == rootValue)
                {
                    rootEncountered = true;
                }
                else if (!rootEncountered)
                {
                    leftInOrder.Add(element);
                    lookupTable[element] = true; //true = left, false = right
                }
                else
                {
                    rightInOrder.Add(element);
                    lookupTable[element] = false;
                }
            }

            List<int> leftPreOrder = new List<int>();
            List<int> rightPreOrder = new List<int>();

            for (int i = 1; i < preOrder.Count; i++)
            {
                int element = preOrder[i];
                if (lookupTable[element])
                {
                    leftPreOrder.Add(element);
                }
                else
                {
                    rightPreOrder.Add(element);
                }
            }

            TreeNode leftChild = ConstructSubtree(leftPreOrder, leftInOrder);
            root.left = leftChild;
            TreeNode rightChild = ConstructSubtree(rightPreOrder, rightInOrder);
            root.right = rightChild;
            return root;
        }

        public TreeNode Solve(int[] param1, int[] param2)
        {
            return BuildTreeBrute(param1, param2);
        }
    }
}