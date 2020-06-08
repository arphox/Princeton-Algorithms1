using System;

namespace Princeton_Algorithms1.Week01
{
    public sealed class WeightedPathCompressingQuickUnionUF : IUnionFindImplementation
    {
        private int[] id;
        private int[] sizes;

        public void Initialize(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value cannot be less than 1.");

            id = new int[n];
            for (int i = 0; i < id.Length; i++)
                id[i] = i;

            sizes = new int[n];
            for (int i = 0; i < sizes.Length; i++)
                sizes[i] = 1;
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
            int rootP = RootOf(p);
            int rootQ = RootOf(q);
            if (rootP == rootQ)
                return;

            if (sizes[rootP] < sizes[rootQ])
            {
                id[rootP] = rootQ;
                sizes[rootQ] += sizes[rootP];
            }
            else
            {
                id[rootQ] = rootP;
                sizes[rootP] += sizes[rootQ];
            }
        }

        public int SizeOf(int x)
        {
            return sizes[x];
        }
    }
}