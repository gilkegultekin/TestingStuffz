using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.BST
{
    public static class BinarySearchTreeGenerator
    {
        public static TreeNode GenerateFromArray(int[] array, bool validateTree = true)
        {
            if (!array.Any())
            {
                return null;
            }

            int index = 0;
            TreeNode root = new TreeNode(array[index]);
            Queue<TreeNode> nodesToProcess = new Queue<TreeNode>();
            nodesToProcess.Enqueue(root);

            while (nodesToProcess.Count > 0)
            {
                TreeNode currentNode = nodesToProcess.Dequeue();

                if (++index < array.Length && array[index] >= 0)
                {
                    if (validateTree && array[index] >= currentNode.val)
                    {
                        throw new ArgumentException("The array does not have a binary tree structure!");
                    }

                    TreeNode newNode = new TreeNode(array[index]);
                    currentNode.left = newNode;
                    nodesToProcess.Enqueue(newNode);
                }

                if (++index < array.Length && array[index] >= 0)
                {
                    if (validateTree && array[index] <= currentNode.val)
                    {
                        throw new ArgumentException("The array does not have a binary tree structure!");
                    }

                    TreeNode newNode = new TreeNode(array[index]);
                    currentNode.right = newNode;
                    nodesToProcess.Enqueue(newNode);
                }
            }

            return root;
        }
    }
}