using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class MaxProfitSolutionTests
    {
        [Theory]
        [InlineData(new[] { 7, 1, 5, 3, 6, 4 }, 7)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 4)]
        [InlineData(new[] { 7, 6, 4, 3, 1 }, 0)]
        public void Test1(int[] prices, int expectedResult)
        {
            MaxProfitSolution solution = new MaxProfitSolution();
            int result = solution.Solve(prices);

            result.Should().Be(expectedResult);
        }
    }
}