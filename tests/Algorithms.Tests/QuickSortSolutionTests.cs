using Algorithms.Tests.Helpers;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms.Tests
{
    public class QuickSortSolutionTests
    {
        [Theory]
        [InlineData(new[] { 4, 3, 1, 6, 2, 5 })]
        [InlineData(new int[] { })]
        [InlineData(new[] { 5 })]
        public void Test1(int[] array)
        {
            QuickSortSolution solution = new QuickSortSolution();

            int[] sortedArray = solution.Solve(array);

            bool isSorted = ArrayHelpers.IsSorted(sortedArray);

            isSorted.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(RandomlyGeneratedArrayData))]
        public void RandomlyGeneratedArrayTest(params int[] array)
        {
            QuickSortSolution solution = new QuickSortSolution();

            int[] sortedArray = solution.Solve(array);

            bool isSorted = ArrayHelpers.IsSorted(sortedArray);

            isSorted.Should().BeTrue();
        }

        public static IEnumerable<object[]> RandomlyGeneratedArrayData => GetData();

        public static IEnumerable<object[]> GetData()
        {
            List<object[]> list = new List<object[]>();
            List<int[]> generatorParameters = new List<int[]> { new[] { 1, 100, 10 }, new[] { 1, 1000, 100 } };

            foreach (var parameters in generatorParameters)
            {
                for (int i = 0; i < 1; i++)
                {
                    int[] array = ArrayHelpers.GenerateIntegerArray(parameters[0], parameters[1], parameters[2]);
                    list.Add(array.Cast<object>().ToArray());
                }
            }

            return list;
        }
    }
}