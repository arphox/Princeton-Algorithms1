using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using Princeton_Algorithms1.Week02.Queue;
using Xunit;

namespace Test.Week02.Queue
{
    public abstract class QueueImplementationTester<TImpl> : IDisposable
        where TImpl : IQueue<int>
    {
        protected abstract IQueue<int> CreateSubject();
        private IQueue<int> subject;

        public QueueImplementationTester()
        {
            subject = CreateSubject();
            PeekAndExpect<InvalidOperationException>();
            DequeueAndExpect<InvalidOperationException>();
        }

        [Fact]
        public void Enqueue1()
        {
            EnqueueDequeueCheck(10);
        }

        [Fact]
        public void Enqueue2()
        {
            Enqueue(5, 3);
            DequeueAndExpect(5);
            PeekAndExpect(3);
        }

        [Fact]
        public void Enqueue5()
        {
            Enqueue(68, -3, 7, 7, 8);
            DequeueAndExpect(68);
            DequeueAndExpect(-3);
            DequeueAndExpect(7);
            DequeueAndExpect(7);
            PeekAndExpect(8);
        }

        [Fact]
        public void Enqueue2Dequeue2()
        {
            EnqueueDequeueCheck(5, 3);
        }

        [Fact]
        public void Enqueue5Dequeue5()
        {
            EnqueueDequeueCheck(68, -3, 7, 7, 8);
        }

        [Fact]
        public void EnqueueDequeue5mix()
        {
            Enqueue(68, -3, 7);
            DequeueAndExpect(68, -3);
            Enqueue(7, 8);
            DequeueAndExpect(7, 7, 8);
        }

        [Fact]
        public void EnqueueDequeue100()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();

            foreach (int item in numbers)
                Enqueue(item);

            for (int i = 0; i < numbers.Length; i++)
            {
                subject.Peek().Should().Be(numbers[i]);
                subject.Dequeue().Should().Be(numbers[i]);
            }
        }

        [Fact]
        public void SmokeTest()
        {
            EnqueueDequeueCheck(10, 20, 30);
            Enqueue(-4, -4, 5);
            DequeueAndExpect(-4);
            PeekAndExpect(-4);  // remained: -4, 5

            Enqueue(101, 102, 103);
            DequeueAndExpect(-4, 5, 101); // remained: 102, 103
            PeekAndExpect(102);

            Enqueue(201);
            DequeueAndExpect(102, 103, 201);
        }

        [Fact]
        public void GenericEnumerableTest()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();

            foreach (int item in numbers)
                subject.Enqueue(item);

            int i = 0;
            foreach (int item in subject)
            {
                item.Should().Be(numbers[i]);
                i++;
            }
            i.Should().Be(numbers.Length);
        }

        [Fact]
        public void NonGenericEnumerableTest()
        {
            int[] numbers = Enumerable.Range(0, 100).ToArray();

            foreach (int item in numbers)
                subject.Enqueue(item);

            int i = 0;
            IEnumerable subjectNonGeneric = subject;
            foreach (int item in subjectNonGeneric)
            {
                item.Should().Be(numbers[i]);
                i++;
            }
            i.Should().Be(numbers.Length);
        }

        public void Dispose()
        {
            while (subject.Count > 0)
                subject.Dequeue();

            PeekAndExpect<InvalidOperationException>();
            DequeueAndExpect<InvalidOperationException>();
        }

        #region [ Helpers ]

        private void Enqueue(params int[] items)
        {
            foreach (int item in items)
            {
                int count = subject.Count;
                subject.Enqueue(item);
                subject.Count.Should().Be(count + 1);
            }
        }

        private void EnqueueDequeueCheck(params int[] items)
        {
            Enqueue(items);
            for (int i = 0; i < items.Length; i++)
                DequeueAndExpect(items[i]);
        }

        private void DequeueAndExpect(params int[] expectedItems)
        {
            foreach (int expectedItem in expectedItems)
            {
                PeekAndExpect(expectedItem);
                int actualItem = subject.Dequeue();
                actualItem.Should().Be(expectedItem);
            }
        }

        private void DequeueAndExpect<TException>()
            where TException : Exception
        {
            Action action = () => subject.Dequeue();
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