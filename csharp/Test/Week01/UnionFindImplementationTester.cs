using System.Linq;
using FluentAssertions;
using Princeton_Algorithms1.Week01;
using Xunit;

namespace Test.Week01
{
    public abstract class UnionFindImplementationTester<TImpl>
        where TImpl : IUnionFindImplementation, new()
    {
        private readonly IUnionFindImplementation subject = new TImpl();

        [Fact]
        public void Test1()
        {
            Init(10);

            Union(0, 5);
            Union(5, 6);
            AssertConnected(0, 6);

            Union(2, 7);
            Union(2, 1);
            AssertConnected(1, 7);
            AssertNotConnectedToAny(1, 0, 5, 6);
            AssertNotConnectedToAny(2, 0, 5, 6);
            AssertNotConnectedToAny(7, 0, 5, 6);

            Union(8, 3);
            Union(4, 9);
            Union(3, 4);
            AssertConnectedToAll(8, 3, 4, 9);
            AssertConnectedToAll(3, 8, 4, 9);
            AssertConnectedToAll(4, 8, 3, 9);
            AssertConnectedToAll(9, 8, 3, 4);
        }

        [Fact]
        public void Test2()
        {
            Init(6);

            Union(0, 1);
            Union(2, 3);
            Union(4, 5);

            Union(0, 4);
            AssertConnectedToAll(1, 0, 4, 5);
            AssertConnectedToAll(0, 1, 4, 5);
            AssertConnectedToAll(4, 1, 0, 5);
            AssertConnectedToAll(5, 1, 0, 4);

            Union(0, 5);
            AssertConnectedToAll(1, 0, 4, 5);
            AssertConnectedToAll(0, 1, 4, 5);
            AssertConnectedToAll(4, 1, 0, 5);
            AssertConnectedToAll(5, 1, 0, 4);

            Union(4, 2);
            for (int i = 0; i <= 5; i++)
                AssertConnectedToAll(i, Enumerable.Range(0, i).ToArray());
        }

        [Fact]
        public void LargeSpaceTest()
        {
            const int Size = 10_000;
            Init(Size);
            Union(0, Size - 1);
            Union(Size / 6, Size / 2);
        }

        private void Init(int n) => subject.Initialize(n);

        private void Union(int p, int q)
        {
            subject.Union(p, q);
            AssertConnected(p, q);
        }

        private void AssertConnected(int p, int q)
        {
            subject.Connected(p, q).Should().BeTrue();
            subject.Connected(q, p).Should().BeTrue();
        }

        private void AssertNotConnected(int p, int q)
        {
            subject.Connected(p, q).Should().BeFalse();
            subject.Connected(q, p).Should().BeFalse();
        }

        private void AssertNotConnectedToAny(int who, params int[] others)
        {
            foreach (int x in others)
                AssertNotConnected(who, x);
        }

        private void AssertConnectedToAll(int who, params int[] others)
        {
            foreach (int x in others)
                AssertConnected(who, x);
        }
    }
}