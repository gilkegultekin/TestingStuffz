using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.HelperClasses
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

        public IEnumerable<int> Values()
        {
            var currentNode = this;
            while (currentNode != null)
            {
                var value = currentNode.val;
                currentNode = currentNode.next;
                yield return value;
            }
        }
    }
}
