using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using Princeton_Algorithms1.Week02.Stack;
using Xunit;

namespace Test.Week02.Stack
{
    public abstract class StackImplementationTester<TImpl> : IDisposable
        where TImpl : IStack<int>
    {
        protected abstract IStack<int> CreateSubject();
        private IStack<int> subject;

        public StackImplementationTester()
        {
            subject = CreateSubject();
            PeekAndExpect<InvalidOperationException>();
            PopAndExpect<InvalidOperationException>();
        }

        [Fact]
        public void Push1()
        {
            Push(10);
        }

        [Fact]
        public void Push2()
        {
            Push(5, 3);
        }

        [Fact]
        public void Push5()
        {
            Push(68, -3, 7, 7, 8);
        }

        [Fact]
        public void Push2Pop2()
        {
            Push(5, 3);
            PopAndExpect(3, 5);
        }

        [Fact]
        public void Push5Pop5()
        {
            Push(68, -3, 7, 7, 8);
            PopAndExpect(8, 7, 7, -3, 68);
        }

        [Fact]
        public void PushPop5mix()
        {
            Push(68, -3, 7);
            PopAndExpect(7, -3);
            Push(7, 8);
            PopAndExpect(8, 7, 68);
        }

        [Fact]
        public void PushPop100()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();

            foreach (int item in numbers)
                Push(item);

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                subject.Peek().Should().Be(numbers[i]);
                subject.Pop().Should().Be(numbers[i]);
            }
        }

        [Fact]
        public void SmokeTest()
        {
            Push(10, 20, 30);
            PopAndExpect(30, 20, 10);
            Push(-4, -4);
            PopAndExpect(-4);
            PeekAndExpect(-4);  // remained: -4

            Push(101, 102, 103);
            PopAndExpect(103, 102); // remained: -4, 101
            PeekAndExpect(101);

            Push(201);
            PopAndExpect(201, 101, -4);
        }

        [Fact]
        public void GenericEnumerableTest()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();

            foreach (int item in numbers)
                subject.Push(item);

            int i = numbers.Length - 1;
            foreach (int item in subject)
            {
                item.Should().Be(numbers[i]);
                i--;
            }
            i.Should().Be(-1);
        }

        [Fact]
        public void NonGenericEnumerableTest()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();

            foreach (int item in numbers)
                subject.Push(item);

            int i = numbers.Length - 1;
            IEnumerable subjectNonGeneric = subject;
            foreach (int item in subjectNonGeneric)
            {
                item.Should().Be(numbers[i]);
                i--;
            }
            i.Should().Be(-1);
        }

        public void Dispose()
        {
            while (subject.Count > 0)
                subject.Pop();

            PeekAndExpect<InvalidOperationException>();
            PopAndExpect<InvalidOperationException>();
        }

        #region [ Helpers ]

        private void Push(params int[] items)
        {
            foreach (int item in items)
            {
                int count = subject.Count;
                subject.Push(item);
                subject.Count.Should().Be(count + 1);
                subject.Peek().Should().Be(item);
                subject.Peek().Should().Be(item);

                int item2 = subject.Pop();
                item2.Should().Be(item);
                subject.Count.Should().Be(count);

                subject.Push(item);
                subject.Count.Should().Be(count + 1);
                subject.Peek().Should().Be(item);
                subject.Peek().Should().Be(item);
            }
        }

        private void PopAndExpect(params int[] expectedItems)
        {
            foreach (int expectedItem in expectedItems)
            {
                int actualItem = subject.Pop();
                actualItem.Should().Be(expectedItem);
            }
        }

        private void PopAndExpect<TException>()
            where TException : Exception
        {
            Action action = () => subject.Pop();
            action.Should().Throw<TException>();
        }

        private void PeekAndExpect(int expectedItem)
        {
            int actualItem = subject.Peek();
            actualItem.Should().Be(expectedItem);
        }

        private void PeekAndExpect<TException>()
            where TException : Exception
        {
            Action action = () => subject.Peek();
            action.Should().Throw<TException>();
        }

        #endregion
    }
}