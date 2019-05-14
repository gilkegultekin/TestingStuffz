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
        //find the root in inorder array
        //everything before the root will be in the left subtree, everything after the root will be in the right subtree
        //the next node in preorder array will be the left child of the root, which is the root of the left subtree
        //if the left child does not exist, then the slice of the inorder array we'll look at will be empty, hence the left child will be null and the recursion will move on to the right child.
        //in the next recursive call, the left child of the root becomes the root and the left child of the new root is the next node in preorder traversal
        //Recurse until you hit a leaf (the slice of the inorder array that we're looking at will be empty)
        //recursion will backtrack to the right child of the current root and move on from there in the same manner
        
        #region FirstSolution
        //public TreeNode BuildTree(int[] preorder, int[] inorder)
        //{
        //    return ConstructSubtree(preorder.ToList(), inorder.ToList());
        //}

        //private TreeNode ConstructSubtree(List<int> preOrder, List<int> inOrder)
        //{
        //    if (preOrder.Count == 0)
        //    {
        //        return null;
        //    }

        //    int rootValue = preOrder[0];
        //    TreeNode root = new TreeNode(rootValue);

        //    if (preOrder.Count == 1)
        //    {
        //        return root;
        //    }

        //    List<int> leftInOrder = new List<int>();
        //    List<int> rightInOrder = new List<int>();
        //    bool rootEncountered = false;
        //    Dictionary<int, bool> lookupTable = new Dictionary<int, bool>();

        //    foreach (int element in inOrder)
        //    {
        //        if (element == rootValue)
        //        {
        //            rootEncountered = true;
        //        }
        //        else if (!rootEncountered)
        //        {
        //            leftInOrder.Add(element);
        //            lookupTable[element] = true; //true = left, false = right
        //        }
        //        else
        //        {
        //            rightInOrder.Add(element);
        //            lookupTable[element] = false;
        //        }
        //    }

        //    List<int> leftPreOrder = new List<int>();
        //    List<int> rightPreOrder = new List<int>();

        //    for (int i = 1; i < preOrder.Count; i++)
        //    {
        //        int element = preOrder[i];
        //        if (lookupTable[element])
        //        {
        //            leftPreOrder.Add(element);
        //        }
        //        else
        //        {
        //            rightPreOrder.Add(element);
        //        }
        //    }

        //    TreeNode leftChild = ConstructSubtree(leftPreOrder, leftInOrder);
        //    root.left = leftChild;
        //    TreeNode rightChild = ConstructSubtree(rightPreOrder, rightInOrder);
        //    root.right = rightChild;
        //    return root;
        //}
        #endregion

        #region SecondSolution

        private readonly Dictionary<int, int> _inOrderIndexMap = new Dictionary<int, int>();
        private int[] _preOrder;
        private int _preIndex;

        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            _preOrder = preorder;

            for (int i = 0; i < inorder.Length; i++)
            {
                _inOrderIndexMap[inorder[i]] = i;
            }

            return ConstructSubtree(0, inorder.Length);
        }

        private TreeNode ConstructSubtree(int inOrderLeft, int inOrderRight)
        {
            //if the current slice of the in order array is empty, then the child does not exist, hence return null
            if (inOrderLeft >= inOrderRight)
            {
                return null;
            }

            //the next node in pre order array is the current root
            int rootValue = _preOrder[_preIndex++];
            TreeNode root = new TreeNode(rootValue);
            //find the root in the inorder array so that you can divide it into left and right subtrees
            int inOrderRootIndex = _inOrderIndexMap[rootValue];

            //the left child of the current root will be the next node in preorder array and its subtree will be the left subtree of the root obviously.
            root.left = ConstructSubtree(inOrderLeft, inOrderRootIndex);
            //this line will only be called after the left subtree of the root has been created.
            root.right = ConstructSubtree(inOrderRootIndex + 1, inOrderRight);

            return root;
        }

        #endregion


        public TreeNode Solve(int[] param1, int[] param2)
        {
            return BuildTree(param1, param2);
        }
    }
}