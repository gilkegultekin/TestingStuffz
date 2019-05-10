using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class MissingNumberSolutionTests
    {
        [Theory]
        [InlineData(new[] { 3, 0, 1 }, 2)]
        [InlineData(new[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8)]
        [InlineData(new[] { 0 }, 1)]
        [InlineData(new[] { 0, 1 }, 2)]
        [InlineData(new[] { 1 }, 0)]
        public void Test_WithSumV2(int[] array, int expectedResult)
        {
            MissingNumberSolution solution = new MissingNumberSolution(MissingNumberStrategy.SumV2);
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(new[] { 3, 0, 1 }, 2)]
        [InlineData(new[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8)]
        [InlineData(new[] { 0 }, 1)]
        [InlineData(new[] { 0, 1 }, 2)]
        [InlineData(new[] { 1 }, 0)]
        public void Test_WithXor(int[] array, int expectedResult)
        {
            MissingNumberSolution solution = new MissingNumberSolution(MissingNumberStrategy.Xor);
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }
    }
}