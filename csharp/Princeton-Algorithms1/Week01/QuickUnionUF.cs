using System;

namespace Princeton_Algorithms1.Week01
{
    public sealed class QuickUnionUF : IUnionFindImplementationLegacy
    {
        private int[] id;

        public void Initialize(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value cannot be less than 1.");

            id = new int[n];
            for (int i = 0; i < id.Length; i++)
                id[i] = i;
        }

        public bool Connected(int p, int q)
        {
            return RootOf(p) == RootOf(q);
        }

        private int RootOf(int x)
        {
            while (x != id[x])
                x = id[x];
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