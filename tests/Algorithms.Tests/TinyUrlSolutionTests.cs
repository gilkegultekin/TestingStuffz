using FluentAssertions;
using Xunit;

namespace Algorithms.Tests
{
    public class TinyUrlSolutionTests
    {
        [Fact]
        public void RandomUrl1()
        {
            string longUrl = "https://leetcode.com/problems/design-tinyurl";

            TinyUrlSolution solution = new TinyUrlSolution();

            string decodedLongUrl = solution.Decode(solution.Encode(longUrl));

            decodedLongUrl.Should().Be(longUrl);
        }
    }
}