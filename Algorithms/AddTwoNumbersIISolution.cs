using Algorithms.HelperClasses;
using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class AddTwoNumbersIISolution : ISolution<ListNode, ListNode, ListNode>
    {
        public ListNode Solve(ListNode l1, ListNode l2)
        {
            return BruteForceWithoutReverse(l1, l2);
        }

        private ListNode BruteForceWithoutReverse(ListNode l1, ListNode l2)
        {
            var left = ConvertListNodeToArray(l1);
            var right = ConvertListNodeToArray(l2);

            var sumArray = SumArrays(left, right);

            return ConvertArrayToLinkedList(sumArray);
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

        private ListNode ConvertArrayToLinkedList(int[] array)
        {
            var headNode = new ListNode(array[0]);
            var currentNode = headNode;

            for (int i = 1; i < array.Length; i++)
            {
                var newNode = new ListNode(array[i]);
                currentNode.next = newNode;
                currentNode = newNode;
            }

            return headNode.val == 0 ? headNode.next : headNode;
        }

        //sum the digits while reading the arrays backwards
        private int[] SumArrays(ValueTuple<int[], int> left, ValueTuple<int[], int> right)
        {
            var leftCounter = left.Item2 - 1;
            var rightCounter = right.Item2 - 1;
            var carryOver = 0;
            var leftArray = left.Item1;
            var rightArray = right.Item1;

            var sumArrayLength = Math.Max(left.Item2, right.Item2) + 1;
            var sumArray = new int[sumArrayLength];
            var sumCounter = sumArrayLength - 1;

            while (leftCounter >= 0 && rightCounter >= 0)
            {
                var leftDigit = leftArray[leftCounter--];
                var rightDigit = rightArray[rightCounter--];
                var sumDigit = AddTwoDigits(leftDigit, rightDigit, ref carryOver);
                sumArray[sumCounter--] = sumDigit;
            }

            if (leftCounter >= 0)
            {
                while (leftCounter >= 0)
                {
                    var sumDigit = AddTwoDigits(leftArray[leftCounter--], 0, ref carryOver);
                    sumArray[sumCounter--] = sumDigit;
                }
            }
            else
            {
                while (rightCounter >= 0)
                {
                    var sumDigit = AddTwoDigits(rightArray[rightCounter--], 0, ref carryOver);
                    sumArray[sumCounter--] = sumDigit;
                }
            }

            sumArray[sumCounter] = carryOver;

            return sumArray;
        }

        private ValueTuple<int[], int> ConvertListNodeToArray(ListNode listNode)
        {
            var array = new int[15];
            var currentNode = listNode;
            var index = 0;

            while (currentNode != null)
            {
                array[index++] = currentNode.val;
                currentNode = currentNode.next;
            }

            return (array, index);
        }

        //first reverse the linked lists
        private ListNode BruteForceWithReverse(ListNode l1, ListNode l2)
        {
            var l1Reverse = ReverseSinglyLinkedList(l1);
            var l2Reverse = ReverseSinglyLinkedList(l2);
            var regularSolution = new AddTwoNumbersSolution();
            return ReverseSinglyLinkedList(regularSolution.Solve(l1Reverse, l2Reverse));
        }

        private ListNode ReverseSinglyLinkedList(ListNode startNode)
        {
            var list = new List<int>();
            foreach (var nodeValue in startNode.Values())
            {
                list.Add(nodeValue);
            }

            var dummyHeadNode = new ListNode(-1);
            var currentNode = dummyHeadNode;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                var newNode = new ListNode(list[i]);
                currentNode.next = newNode;
                currentNode = newNode;
            }

            return dummyHeadNode.next;
        }
    }
}
