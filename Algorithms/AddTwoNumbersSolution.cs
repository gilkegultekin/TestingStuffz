using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    //You are given two non-empty linked lists representing two non-negative integers. The digits are stored 
    //in reverse order and each of their nodes contain a single digit. Add the two numbers and return it as a 
    //linked list.
    //You may assume the two numbers do not contain any leading zero, except the number 0 itself.
    //Example:
    //Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
    //Output: 7 -> 0 -> 8
    //Explanation: 342 + 465 = 807.
    public class AddTwoNumbersSolution : ISolution<ListNode, ListNode, ListNode>
    {
        public ListNode Solve(ListNode l1, ListNode l2)
        {
            return BruteForceSingleLoop(l1, l2);
        }

        private ListNode BruteForceSingleLoop(ListNode l1, ListNode l2)
        {
            var dummyHeadNode = new ListNode(0);
            var currentNode = dummyHeadNode;
            var carryOver = 0;
            var currL1 = l1;
            var currL2 = l2;
            int digit;
            ListNode newNode;

            while (currL1 != null && currL2 != null)
            {
                digit = AddTwoDigits(currL1.val, currL2.val, ref carryOver);
                newNode = new ListNode(digit);
                currentNode.next = newNode;

                currentNode = newNode;
                currL1 = currL1.next;
                currL2 = currL2.next;
            }

            if (currL1 == null && currL2 == null)
            {
                if (carryOver == 1)
                {
                    newNode = new ListNode(1);
                    currentNode.next = newNode;
                }

                return dummyHeadNode.next;
            }

            var remaining = currL1 ?? currL2;

            while (remaining != null)
            {
                digit = AddTwoDigits(0, remaining.val, ref carryOver);
                newNode = new ListNode(digit);
                currentNode.next = newNode;
                currentNode = newNode;
                remaining = remaining.next;
            }

            if (carryOver == 1)
            {
                newNode = new ListNode(1);
                currentNode.next = newNode;
            }

            return dummyHeadNode.next;
        }

        private int AddTwoDigits(int d1, int d2, ref int carryOver)
        {
            int digit;
            var sum = carryOver + d1 + d2;

            if (sum >= 10)
            {
                digit = sum % 10;
                carryOver = 1;
            }
            else
            {
                digit = sum;
                carryOver = 0;
            }

            return digit;
        }
    }

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
