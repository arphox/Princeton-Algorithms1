using System;

namespace Princeton_Algorithms1.Week01.InterviewQuestions
{
    /// <summary>
    ///     Successor with delete.
    ///     Given a set of n integers S={0,1,...,n−1} and a sequence of requests of the following form:
    ///     
    ///     - Remove x from S
    ///     - Find the successor of x: the smallest y in S such that y ≥ x.
    ///     
    ///     design a data type so that all operations (except construction)
    ///     take logarithmic time or better in the worst case.
    /// </summary>
    public sealed class _03_SuccessorWithDelete
    {
        private readonly int[] id;

        public _03_SuccessorWithDelete(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value cannot be less than 1.");

            id = new int[n];
            for (int i = 0; i < id.Length; i++)
                id[i] = i;
        }

        public void Remove(int x)
        {
            Union(x, x + 1);
        }

        public int SuccessorOf(int x)
        {
            return RootOf(x + 1);
        }

        public bool Connected(int p, int q)
        {
            return RootOf(p) == RootOf(q);
        }

        private int RootOf(int x)
        {
            while (x != id[x])
            {
                id[x] = id[id[x]];
                x = id[x];
            }
            return x;
        }

        public void Union(int p, int q)
        {
            int pRoot = RootOf(p);
            int qRoot = RootOf(q);
            id[pRoot] = qRoot;
        }
    }
}