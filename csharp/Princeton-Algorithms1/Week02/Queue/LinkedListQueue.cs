using System;
using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Queue
{
    public sealed class LinkedListQueue<T> : IQueue<T>
    {
        private Node? first;
        private Node? last;

        public int Count { get; private set; } = 0;

        public void Enqueue(T item)
        {
            var newNode = new Node { value = item };
            if (first == null)
                first = newNode;
            if (last == null)
                last = newNode;
            else
            {
                last.next = newNode;
                last = newNode;
            }
            Count++;
        }

        public T Dequeue()
        {
            if (first == null)
                throw EmptyQueue();

            T item = first.value;
            first = first.next;
            Count--;
            return item;
        }

        public T Peek()
        {
            if (Count == 0)
                throw EmptyQueue();

            return first.value;
        }

        public IEnumerator<T> GetEnumerator() => GetAllItems().GetEnumerator();

        private IEnumerable<T> GetAllItems()
        {
            Node temp = first;
            while (temp != null)
            {
                yield return temp.value;
                temp = temp.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private InvalidOperationException EmptyQueue()
        {
            return new InvalidOperationException("Queue is empty");
        }

        private sealed class Node
        {
            internal T value;
            internal Node? next;
        }
    }
}