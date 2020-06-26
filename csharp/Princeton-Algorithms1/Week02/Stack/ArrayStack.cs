using System;
using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Stack
{
    public sealed class ArrayStack<T> : IStack<T>
    {
        public const int DefaultCapacity = 4;
        private readonly bool shrinkOnPop;
        private T[] items;
        private int currentIndex = -1;
        public int Count => currentIndex + 1;
        public int Capacity => items.Length;

        public ArrayStack(int initialCapacity = DefaultCapacity, bool shrinkOnPop = true)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(initialCapacity), initialCapacity, "Value cannot be negative.");
            items = new T[initialCapacity];
            this.shrinkOnPop = shrinkOnPop;
        }

        public T Peek()
        {
            if (currentIndex < 0)
                throw EmptyStack();
            return items[currentIndex];
        }

        public T Pop()
        {
            if (currentIndex < 0)
                throw EmptyStack();
            T item = items[currentIndex];
            items[currentIndex--] = default; // release possible unused references
            if (shrinkOnPop && Count < items.Length / 4)
                Resize(items.Length / 2);
            return item;
        }

        public void Push(T item)
        {
            if (Count >= items.Length)
                Resize(items.Length * 2);

            items[++currentIndex] = item;
        }

        public IEnumerator<T> GetEnumerator() => GetAllItems().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<T> GetAllItems()
        {
            for (int i = currentIndex; i >= 0; i--)
                yield return items[i];
        }

        private InvalidOperationException EmptyStack()
        {
            return new InvalidOperationException("Stack is empty");
        }

        private void Resize(int newSize)
        {
            Array.Resize(ref items, (items.Length == 0) ? DefaultCapacity : newSize);
        }
    }
}