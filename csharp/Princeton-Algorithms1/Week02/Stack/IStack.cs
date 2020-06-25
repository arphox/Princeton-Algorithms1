using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Stack
{
    public interface IStack : IStack<object>
    {
    }

    public interface IStack<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        void Push(T item);
        T Pop();
        T Peek();
    }
}