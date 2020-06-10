using FluentAssertions;
using Princeton_Algorithms1.Week01;
using Xunit;

namespace Test.Week01
{
    public abstract class UnionFindImplementationTester<TImpl>
        where TImpl : IUnionFindImplementationLegacy, new()
    {
        protected readonly IUnionFindImplementationLegacy subject = new TImpl();

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
            AssertAllConnectedToAll(8, 3, 4, 9);
        }

        [Fact]
        public void Test2()
        {
            Init(6);

            Union(0, 1);
            AssertNotConnectedToAny(0, 2, 3, 4, 5);
            AssertNotConnectedToAny(1, 2, 3, 4, 5);

            Union(2, 3);
            AssertNotConnectedToAny(2, 0, 1, 4, 5);
            AssertNotConnectedToAny(3, 0, 1, 4, 5);

            Union(4, 5);
            AssertNotConnectedToAny(4, 0, 1, 2, 3);
            AssertNotConnectedToAny(5, 0, 1, 2, 3);

            Union(0, 4);
            AssertAllConnectedToAll(1, 0, 4, 5);
            AssertNotConnectedToAny(2, 0, 1, 4, 5);
            AssertNotConnectedToAny(3, 0, 1, 4, 5);

            Union(0, 5);
            AssertAllConnectedToAll(1, 0, 4, 5);

            Union(4, 2);
            AssertAllConnectedToAll(0, 1, 2, 3, 4, 5);
        }

        [Fact]
        public void Test3()
        {
            Init(10);
            Union(4, 3);
            Union(3, 8);
            AssertConnected(3, 8);

            Union(6, 5);

            Union(9, 4);
            AssertAllConnectedToAll(3, 4, 8, 9);

            Union(2, 1);

            Union(5, 0);
            AssertConnected(0, 6);

            Union(7, 2);
            AssertConnected(7, 1);

            Union(6, 1);
            AssertAllConnectedToAll(0, 1, 2, 5, 6, 7);

            Union(7, 3);
            AssertAllConnectedToAll(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }

        [Fact]
        public void LargeSpaceTest()
        {
            const int Size = 10_000;
            Init(Size);
            Union(0, Size - 1);
            Union(Size / 6, Size / 2);
        }

        protected void Init(int n) => subject.Initialize(n);

        protected void Union(int p, int q)
        {
            subject.Union(p, q);
            AssertConnected(p, q);
        }

        protected void UnionAll(params int[] values)
        {
            for (int i = 0; i < values.Length - 1; i++)
                Union(values[i], values[i + 1]);
        }

        protected void AssertConnected(int p, int q)
        {
            subject.Connected(p, q).Should().BeTrue();
            subject.Connected(q, p).Should().BeTrue();
        }

        protected void AssertNotConnected(int p, int q)
        {
            subject.Connected(p, q).Should().BeFalse();
            subject.Connected(q, p).Should().BeFalse();
        }

        protected void AssertNotConnectedToAny(int who, params int[] others)
        {
            foreach (int x in others)
                AssertNotConnected(who, x);
        }

        protected void AssertAllConnectedToAll(params int[] ids)
        {
            foreach (int x1 in ids)
                foreach (int x2 in ids)
                    AssertConnected(x1, x2);
        }
    }
}