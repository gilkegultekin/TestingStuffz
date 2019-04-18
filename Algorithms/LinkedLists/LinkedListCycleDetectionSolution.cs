using System;
using System.Collections.Generic;

namespace Algorithms.LinkedLists
{
    public class LinkedListCycleDetectionSolution : ISolution<bool, ListNode>
    {
        public bool HasCycle(ListNode head)
        {
            HashSet<ListNode> nodeHashSet = new HashSet<ListNode>();

            ListNode current = head;
            nodeHashSet.Add(current);

            while(current.next != null){
                current = current.next;
                if (nodeHashSet.Contains(current)){
                    return true;
                }
                else{
                    nodeHashSet.Add(current);
                }
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
