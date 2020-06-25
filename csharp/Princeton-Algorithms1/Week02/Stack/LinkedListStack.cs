using System;
using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Stack
{
    public sealed class LinkedListStack<T> : IStack<T>
    {
        private Node? first;

        public int Count { get; private set; } = 0;

        public IEnumerator<T> GetEnumerator() => IterateAllItems().GetEnumerator();

        private IEnumerable<T> IterateAllItems()
        {
            Node? current = first;
            while (current != null)
            {
                yield return current.value;
                current = current.next;
            }
        }

        public T Peek()
        {
            if (first == null)
                throw EmptyStack();
            return first.value;
        }

        public T Pop()
        {
            if (first == null)
                throw EmptyStack();

            T item = first.value;
            first = first.next;
            Count--;
            return item;
        }

        public void Push(T item)
        {
            var newNode = new Node { value = item };
            newNode.next = first;
            first = newNode;
            Count++;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private InvalidOperationException EmptyStack()
        {
            return new InvalidOperationException("Stack is empty");
        }

        private sealed class Node
        {
            internal T value;
            internal Node? next;
        }
    }
}