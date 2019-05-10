using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class MissingNumberSolutionTests
    {
        [Theory]
        [InlineData(new[] { 3, 0, 1 }, 2, MissingNumberStrategy.SumV2)]
        [InlineData(new[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8, MissingNumberStrategy.SumV2)]
        [InlineData(new[] { 0 }, 1, MissingNumberStrategy.SumV2)]
        [InlineData(new[] { 0, 1 }, 2, MissingNumberStrategy.SumV2)]
        [InlineData(new[] { 1 }, 0, MissingNumberStrategy.SumV2)]
        public void Test_WithSumV2(int[] array, int expectedResult, MissingNumberStrategy strategy)
        {
            MissingNumberSolution solution = new MissingNumberSolution(strategy);
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(new[] { 3, 0, 1 }, 2, MissingNumberStrategy.Xor)]
        [InlineData(new[] { 9, 6, 4, 2, 3, 5, 7, 0, 1 }, 8, MissingNumberStrategy.Xor)]
        [InlineData(new[] { 0 }, 1, MissingNumberStrategy.Xor)]
        [InlineData(new[] { 0, 1 }, 2, MissingNumberStrategy.Xor)]
        [InlineData(new[] { 1 }, 0, MissingNumberStrategy.Xor)]
        public void Test_WithXor(int[] array, int expectedResult, MissingNumberStrategy strategy)
        {
            MissingNumberSolution solution = new MissingNumberSolution(strategy);
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }
    }
}