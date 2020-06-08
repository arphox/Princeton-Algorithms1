using System;

namespace Princeton_Algorithms1.Week01
{
    public class QuickFindUF : IUnionFindImplementation
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
            return id[p] == id[q];
        }

        public void Union(int p, int q)
        {
            int pid = id[p];
            int qid = id[q];
            for (int i = 0; i < id.Length; i++)
            {
                if (id[i] == pid)
                    id[i] = qid;
            }
        }
    }
}