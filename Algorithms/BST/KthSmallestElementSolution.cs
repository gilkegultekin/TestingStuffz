namespace Algorithms.BST
{
    /// <summary>
    /// 230. Kth Smallest Element in a BST
    /// Given a binary search tree, write a function kthSmallest to find the kth smallest element in it.
    /// 
    /// Note: 
    /// You may assume k is always valid, 1 ≤ k ≤ BST's total elements.
    /// 
    /// Example 1:
    /// 
    /// Input: root = [3,1,4,null,2], k = 1
    /// 3
    /// / \
    /// 1   4
    /// \
    /// 2
    /// Output: 1
    /// Example 2:
    /// 
    /// Input: root = [5,3,6,2,4,null,null,1], k = 3
    /// 5
    /// / \
    /// 3   6
    /// / \
    /// 2   4
    /// /
    /// 1
    /// Output: 3
    /// Follow up:
    /// What if the BST is modified (insert/delete operations) often and you need to find the kth smallest frequently? How would you optimize the kthSmallest routine?
    /// </summary>
    public class KthSmallestElementSolution : ISolution<int, TreeNode, int>
    {
        public int KthSmallest(TreeNode root, int k)
        {
            AugmentedTreeNode augmentedRoot = AugmentedTreeNode.CreateAugmentedTreeNode(root, null , false);

            AugmentedTreeNode currentNode = augmentedRoot;

            while (currentNode.Order != k)
            {
                currentNode = currentNode.Order > k ? currentNode.Left : currentNode.Right;
            }

            return currentNode.Value;
        }

        public int Solve(TreeNode param1, int param2)
        {
            return KthSmallest(param1, param2);
        }

        private class AugmentedTreeNode
        {
            private int NumberOfNodesInSubtree { get; set; }
            
            private int _order;

            public int Order => _order != 0 ? _order : (_order = CalculateOrder());

            public AugmentedTreeNode Left { get; private set; }

            public AugmentedTreeNode Right { get; private set; }

            private AugmentedTreeNode Parent { get; set; }

            public int Value { get; private set; }

            private bool IsLeftChild { get; set; }

            private bool IsRoot { get; set; }

            public static AugmentedTreeNode CreateAugmentedTreeNode(TreeNode simpleNode, AugmentedTreeNode parentNode, bool isLeftChild)
            {
                if (simpleNode == null)
                {
                    return null;
                }

                AugmentedTreeNode current = new AugmentedTreeNode();

                current.Parent = parentNode;
                current.IsRoot = parentNode == null;
                current.Value = simpleNode.val;
                current.Left = CreateAugmentedTreeNode(simpleNode.left, current, true);
                current.Right = CreateAugmentedTreeNode(simpleNode.right, current, false);
                current.IsLeftChild = isLeftChild;
                current.CalculateNodesInSubtree();

                return current;
            }

            private void CalculateNodesInSubtree()
            {
                NumberOfNodesInSubtree = (Left?.NumberOfNodesInSubtree ?? 0) + (Right?.NumberOfNodesInSubtree ?? 0) + 1;
            }

            private int CalculateOrder()
            {
                AugmentedTreeNode rightSubtreeRoot = FindRightSubtreeRoot();
                return (Left?.NumberOfNodesInSubtree ?? 0) +
                       (rightSubtreeRoot?.Order ?? 0) + 1;
            }

            private AugmentedTreeNode FindRightSubtreeRoot()
            {
                if (IsRoot)
                {
                    return null;
                }

                if (!IsLeftChild)
                {
                    return Parent;
                }

                AugmentedTreeNode currentParent = Parent;

                while (!currentParent.IsRoot && currentParent.IsLeftChild)
                {
                    currentParent = currentParent.Parent;
                }

                return currentParent.IsRoot ? null : currentParent.Parent;
            }
        }
    }
}