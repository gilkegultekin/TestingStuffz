using System.Collections.Generic;

namespace Algorithms.LinkedLists
{
    public class LinkedListCycleDetectionSolution : ISolution<bool, ListNode>
    {
        public bool HasCycle(ListNode head)
        {
            if (head == null)
            {
                return false;
            }

            ListNode slow = head.next;
            ListNode fast = head.next?.next;

            while (fast != null)
            {
                if (slow == fast)
                {
                    return true;
                }

                slow = slow.next;
                fast = fast.next?.next;
            }

            return false;
        }

        public bool HasCycle_HashSet(ListNode head)
        {
            HashSet<ListNode> nodeHashSet = new HashSet<ListNode>();

            ListNode current = head;
            while (current != null)
            {
                if (!nodeHashSet.Add(current))
                {
                    return true;
                }

                current = current.next;
            }

            return false;
        }

        public bool Solve(ListNode param)
        {
            return HasCycle(param);
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
}
