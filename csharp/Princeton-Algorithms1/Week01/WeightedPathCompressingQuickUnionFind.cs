using System;

namespace Princeton_Algorithms1.Week01
{
    public sealed class WeightedPathCompressingQuickUnionFind : IUnionFindImplementationLegacy, IUnionFindImplementation
    {
        private int[] id;
        private int[] sizes;
        private int[] maxElement;

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

            maxElement = new int[n];
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

            int higher = Math.Max(p, q);
            if (higher > maxElement[rootP])
                maxElement[rootP] = higher;
        }

        public int Find(int x)
        {
            return maxElement[RootOf(x)];
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
    }
}
