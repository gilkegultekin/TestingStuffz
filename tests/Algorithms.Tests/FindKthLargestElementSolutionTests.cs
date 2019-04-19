using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class FindKthLargestElementSolutionTests
    {
        [Fact]
        public void CustomArray1()
        {
            const int k = 2;
            int[] array = { 3, 2, 1, 5, 6, 4 };
            const int expectedResult = 5;

            FindKthLargestElementSolution solution = new FindKthLargestElementSolution();
            int result = solution.Solve(array, k);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CustomArray2()
        {
            const int k = 4;
            int[] array = { 3, 2, 3, 1, 2, 4, 5, 5, 6 };
            const int expectedResult = 4;

            FindKthLargestElementSolution solution = new FindKthLargestElementSolution();
            int result = solution.Solve(array, k);

            result.Should().Be(expectedResult);
        }
    }
}