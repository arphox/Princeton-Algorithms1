namespace Princeton_Algorithms1.Week01
{
    public interface IUnionFindImplementationLegacy
    {
        void Initialize(int n);
        bool Connected(int p, int q);
        void Union(int p, int q);
    }
}