using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class CountingInversionsSolutionTests
    {
        [Theory]
        [InlineData(new[] { 1, 3, 5, 2, 4, 6 }, 3)]
        [InlineData(new[] { 6, 5, 4, 3, 2, 1 }, 15)]
        [InlineData(new[] { 10 }, 0)]
        [InlineData(new int[] { }, 0)]
        [InlineData(null, 0)]
        [InlineData(new[] { 10, 4, 2, 8, 1, 6, 3, 7, 9, 5 }, 22)]
        public void Test1(int[] array, int expectedResult)
        {
            CountingInversionsSolution solution = new CountingInversionsSolution();

            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }
    }
}