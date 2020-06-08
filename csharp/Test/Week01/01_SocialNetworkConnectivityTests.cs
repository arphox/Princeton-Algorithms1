using System;
using System.Collections.Generic;
using FluentAssertions;
using Princeton_Algorithms1.Week01.InterviewQuestions;
using Xunit;

namespace Test.Week01
{
    public class _01_SocialNetworkConnectivityTests
    {
        [Fact]
        public void Test()
        {
            var sut = new _01_SocialNetworkConnectivity(7, RelationProvider());
            int result = sut.Run();
            result.Should().Be(8);
        }

        private IEnumerable<(int, int)> RelationProvider()
        {
            yield return (0, 1);
            yield return (0, 4);
            yield return (1, 2);
            yield return (4, 6);
            yield return (0, 6);
            yield return (6, 5);
            yield return (0, 2);
            yield return (1, 3);
            throw new Exception("It's too late!");
        }
    }
}