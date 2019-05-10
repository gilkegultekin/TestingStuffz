using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class SingleNumberSolutionTests
    {
        [Fact]
        public void Array1()
        {
            int[] array = {2,2,1};
            const int expectedResult = 1;

            SingleNumberSolution solution = new SingleNumberSolution();
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Array2()
        {
            int[] array = { 4, 1, 2, 1, 2 };
            const int expectedResult = 4;

            SingleNumberSolution solution = new SingleNumberSolution();
            int result = solution.Solve(array);

            result.Should().Be(expectedResult);
        }
    }
}