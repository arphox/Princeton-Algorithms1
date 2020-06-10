﻿namespace Princeton_Algorithms1.Week01
{
    public interface IUnionFindImplementation
    {
        void Initialize(int n);
        int Find(int n);
        void Union(int p, int q);
    }
}