using System;
using Algorithms.LinkedLists;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class LinkedListCycleDetectionSolutionTests
    {
        [Fact]
        public void SingleNodeList_ShouldNotDetectCycle(){
            ListNode head = new ListNode(3);

            LinkedListCycleDetectionSolution solution = new LinkedListCycleDetectionSolution();
            bool hasCycle = solution.Solve(head);

            hasCycle.Should().BeFalse();
        }

        [Fact]
        public void TwoNodeList_ShouldNotDetectCycle()
        {
            ListNode head = new ListNode(3);
            ListNode second = new ListNode(4);
            head.next = second;

            LinkedListCycleDetectionSolution solution = new LinkedListCycleDetectionSolution();
            bool hasCycle = solution.Solve(head);

            hasCycle.Should().BeFalse();
        }

        [Fact]
        public void TwoNodeList_ShouldDetectCycle()
        {
            ListNode head = new ListNode(3);
            ListNode second = new ListNode(4);
            head.next = second;
            second.next = head;

            LinkedListCycleDetectionSolution solution = new LinkedListCycleDetectionSolution();
            bool hasCycle = solution.Solve(head);

            hasCycle.Should().BeTrue();
        }
    }
}
