using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Queue
{
    public interface IQueue : IQueue<object>
    {
    }

    public interface IQueue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }
}