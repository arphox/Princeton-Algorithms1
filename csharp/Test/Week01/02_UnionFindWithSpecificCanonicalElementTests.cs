using FluentAssertions;
using Princeton_Algorithms1.Week01.InterviewQuestions;
using Xunit;

namespace Test.Week01
{
    public sealed class _02_UnionFindWithSpecificCanonicalElementTests :
        UnionFindImplementationTester<_02_UnionFindWithSpecificCanonicalElement>
    {
        private _02_UnionFindWithSpecificCanonicalElement sut;

        [Fact]
        public void Test()
        {
            sut = (_02_UnionFindWithSpecificCanonicalElement)subject;

            Init(8);

            UnionAll(6, 2, 0, 5);
            ExpectFoundForAll(6, 0, 2, 6, 5);

            UnionAll(3, 4, 1, 7);
            ExpectFoundForAll(7, 3, 7, 1, 4);
        }

        private void ExpectFoundForAll(int expectedValue, params int[] testedValues)
        {
            foreach (int value in testedValues)
                sut.Find(value).Should().Be(expectedValue);
        }
    }
}