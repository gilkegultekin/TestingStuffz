using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class SingleNumberSolutionTests
    {
        [Theory]
        [InlineData(new []{ 2, 2, 1 }, 1)]
        [InlineData(new[] { 4, 1, 2, 1, 2 }, 4)]
        public void Test1(int[] array, int expectedResult)
        {
            SingleNumberSolution solution = new SingleNumberSolution();
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }
    }
}