using FluentAssertions;
using Princeton_Algorithms1.Week01.InterviewQuestions;
using Xunit;

namespace Test.Week01
{
    public class _03_SuccessorWithDeleteTests
    {
        [Fact]
        public void Test()
        {
            const int capacity = 8;
            _03_SuccessorWithDelete sut = new _03_SuccessorWithDelete(capacity);
            for (int i = 0; i < capacity - 1; i++)
                sut.SuccessorOf(i).Should().Be(i + 1);

            sut.Remove(3);
            sut.SuccessorOf(2).Should().Be(4);

            sut.Remove(2);
            sut.SuccessorOf(1).Should().Be(4);

            sut.Remove(4);
            sut.SuccessorOf(1).Should().Be(5);

            sut.Remove(6);
            sut.SuccessorOf(5).Should().Be(7);

            sut.Remove(5);
            sut.SuccessorOf(1).Should().Be(7);
            sut.SuccessorOf(0).Should().Be(1);
        }
    }
}