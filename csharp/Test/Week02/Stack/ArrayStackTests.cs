using System;
using FluentAssertions;
using Princeton_Algorithms1.Week02.Stack;
using Xunit;

namespace Test.Week02.Stack
{
    public sealed partial class StackImplementationTests
    {
        public class ArrayStackTests : QueueImplementationTester<ArrayStack<int>>
        {
            protected override IStack<int> CreateSubject() => new ArrayStack<int>();

            [Theory]
            [InlineData(-1)]
            [InlineData(-3)]
            public void Constructor_should_throw_on_negative_capacity(int capacity)
            {
                Action act = () => new ArrayStack<string>(capacity);

                act.Should().Throw<ArgumentOutOfRangeException>()
                    .WithMessage("*capacity*");
            }
        }
    }
}