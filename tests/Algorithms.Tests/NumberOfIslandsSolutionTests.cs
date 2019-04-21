using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class NumberOfIslandsSolutionTests
    {
        /// Input:
        /// 11110
        /// 11010
        /// 11000
        /// 00000
        [Fact]
        public void CustomGrid1()
        {
            const int expectedResult = 1;
            char[][] grid =
            {
                new[] { '1', '1', '1', '1', '0' },
                new[] { '1', '1', '0', '1', '0' },
                new[] { '1', '1', '0', '0', '0' },
                new[] { '0', '0', '0', '0', '0' }
            };

            NumberOfIslandsSolution solution = new NumberOfIslandsSolution();
            int numberOfIslands = solution.Solve(grid);

            numberOfIslands.Should().Be(expectedResult);
        }

        /// Input:
        /// 11000
        /// 11000
        /// 00100
        /// 00011
        [Fact]
        public void CustomGrid2()
        {
            const int expectedResult = 3;
            char[][] grid =
            {
                new[] { '1', '1', '0', '0', '0' },
                new[] { '1', '1', '0', '0', '0' },
                new[] { '0', '0', '1', '0', '0' },
                new[] { '0', '0', '0', '1', '1' }
            };

            NumberOfIslandsSolution solution = new NumberOfIslandsSolution();
            int numberOfIslands = solution.Solve(grid);

            numberOfIslands.Should().Be(expectedResult);
        }

        /// Input:
        /// 111
        /// 010
        /// 111
        [Fact]
        public void CustomGrid3()
        {
            const int expectedResult = 1;
            char[][] grid =
            {
                new[] { '1', '1', '1' },
                new[] { '0', '1', '0' },
                new[] { '1', '1', '1' },
            };

            NumberOfIslandsSolution solution = new NumberOfIslandsSolution();
            int numberOfIslands = solution.Solve(grid);

            numberOfIslands.Should().Be(expectedResult);
        }
    }
}