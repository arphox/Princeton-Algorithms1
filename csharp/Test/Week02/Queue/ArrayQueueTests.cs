using System;
using FluentAssertions;
using Princeton_Algorithms1.Week02.Queue;
using Xunit;

namespace Test.Week02.Queue
{
    public sealed partial class QueueImplementationTests
    {
        public class ArrayQueueTests : QueueImplementationTester<ArrayQueue<int>>
        {
            protected override IQueue<int> CreateSubject() => new ArrayQueue<int>();

            [Theory]
            [InlineData(-1)]
            [InlineData(-3)]
            public void Constructor_should_throw_on_negative_capacity(int capacity)
            {
                Action act = () => new ArrayQueue<string>(capacity);

                act.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("*capacity*");
            }
        }
    }
}