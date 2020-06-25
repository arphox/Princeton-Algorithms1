using System.Collections;
using System.Collections.Generic;

namespace Princeton_Algorithms1.Week02.Stack
{
    public sealed class LinkedListStack<T> : IStack<T>
    {
        private Stack<T> stack = new Stack<T>();

        public int Count => stack.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return stack.GetEnumerator();
        }

        public T Peek()
        {
            return stack.Peek();
        }

        public T Pop()
        {
            return stack.Pop();
        }

        public void Push(T item)
        {
            stack.Push(item);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}