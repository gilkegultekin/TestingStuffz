using Algorithms.Heaps;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class KthLargestElementInStreamSolutionTests
    {
        [Fact]
        public void Test1()
        {
            const int k = 3;
            int[] array = { 4, 5, 8, 2 };

            KthLargestElementInStreamSolution solution = new KthLargestElementInStreamSolution(k, array);

            int result;
            result = solution.Add(3);
            result.Should().Be(4);

            result = solution.Add(5);
            result.Should().Be(5);

            result = solution.Add(10);
            result.Should().Be(5);

            result = solution.Add(9);
            result.Should().Be(8);

            result = solution.Add(4);
            result.Should().Be(8);
        }

        [Fact]
        public void Test2()
        {
            const int k = 1;
            int[] array = { };

            KthLargestElementInStreamSolution solution = new KthLargestElementInStreamSolution(k, array);

            int result;
            result = solution.Add(-3);
            result.Should().Be(-3);

            result = solution.Add(-2);
            result.Should().Be(-2);

            result = solution.Add(-4);
            result.Should().Be(-2);

            result = solution.Add(0);
            result.Should().Be(0);

            result = solution.Add(4);
            result.Should().Be(4);
        }

        [Fact]
        public void Test3()
        {
            const int k = 2;
            int[] array = { 0 };

            KthLargestElementInStreamSolution solution = new KthLargestElementInStreamSolution(k, array);

            int result;
            result = solution.Add(-1);
            result.Should().Be(-1);

            result = solution.Add(1);
            result.Should().Be(0);

            result = solution.Add(-2);
            result.Should().Be(0);

            result = solution.Add(-4);
            result.Should().Be(0);

            result = solution.Add(3);
            result.Should().Be(1);
        }
    }
}