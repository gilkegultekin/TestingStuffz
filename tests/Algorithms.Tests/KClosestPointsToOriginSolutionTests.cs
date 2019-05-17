using Algorithms.Heaps;
using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class KClosestPointsToOriginSolutionTests
    {
        [Fact]
        public void Test1()
        {
            int[][] points = { new[] { 1, 3 }, new[] { -2, 2 } };
            const int k = 1;
            int[][] expectedResult = {new[] {-2, 2}};
            KClosestPointsToOriginSolution solution = new KClosestPointsToOriginSolution();

            int[][] result = solution.Solve(points, k);
            result.Should().NotBeNull();
            result.Length.Should().Be(expectedResult.Length);

            foreach (int[] point in expectedResult)
            {
                result.Should().ContainEquivalentOf(point);
            }
        }

        [Fact]
        public void Test2()
        {
            int[][] points = { new[] { 3, 3 }, new[] { 5, -1 }, new[] { -2, 4 } };
            const int k = 2;
            int[][] expectedResult = { new[] { -2, 4 }, new[] { 3, 3 } };
            KClosestPointsToOriginSolution solution = new KClosestPointsToOriginSolution();

            int[][] result = solution.Solve(points, k);
            result.Should().NotBeNull();
            result.Length.Should().Be(expectedResult.Length);

            foreach (int[] point in expectedResult)
            {
                result.Should().ContainEquivalentOf(point);
            }
        }

        [Fact]
        public void Test3()
        {
            int[][] points = { new[] { 6, 10 }, new[] { -3, 3 }, new[] { -2, 5 }, new[] { 0, 2 } };
            const int k = 3;
            int[][] expectedResult = { new[] { 0, 2 }, new[] { -3, 3 }, new[] { -2, 5 } };
            KClosestPointsToOriginSolution solution = new KClosestPointsToOriginSolution();

            int[][] result = solution.Solve(points, k);
            result.Should().NotBeNull();
            result.Length.Should().Be(expectedResult.Length);

            foreach (int[] point in expectedResult)
            {
                result.Should().ContainEquivalentOf(point);
            }
        }
    }
}