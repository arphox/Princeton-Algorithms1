using System;
using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Queue
{
    public sealed class ArrayQueue<T> : IQueue<T>
    {
        public const int DefaultCapacity = 4;
        private int head;
        private int tail;
        private T[] items;

        public ArrayQueue(int initialCapacity = DefaultCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), initialCapacity, "Value cannot be negative.");
            items = new T[initialCapacity];
        }

        public int Count { get; private set; } = 0;

        public void Enqueue(T item)
        {
            if (Count >= items.Length)
                Resize(items.Length * 2);

            if (tail >= items.Length)
                tail = 0;

            items[tail] = item;
            tail++;
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw EmptyQueue();

            var item = items[head];
            items[head] = default; // release possible unused references
            head++;
            if (head >= items.Length)
                head = 0;
            Count--;
            return item;
        }

        public T Peek()
        {
            if (Count == 0)
                throw EmptyQueue();

            return items[head];
        }

        public IEnumerator<T> GetEnumerator() => GetAllItems().GetEnumerator();

        private IEnumerable<T> GetAllItems()
        {
            for (int i = head; i < tail; i++)
                yield return items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private InvalidOperationException EmptyQueue()
        {
            return new InvalidOperationException("Queue is empty");
        }

        private void Resize(int newSize)
        {
            Array.Resize(ref items, (items.Length == 0) ? DefaultCapacity : newSize);
        }
    }
}