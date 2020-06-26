using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Queue
{
    public sealed class ArrayQueue<T> : IQueue<T>
    {
        private Queue<T> q = new Queue<T>();

        public int Count => q.Count;

        public T Dequeue()
        {
            return q.Dequeue();
        }

        public void Enqueue(T item)
        {
            q.Enqueue(item);
        }

        public T Peek()
        {
            return q.Peek();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return q.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}