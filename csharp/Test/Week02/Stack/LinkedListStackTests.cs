using Princeton_Algorithms1.Week02.Stack;

namespace Test.Week02.Stack
{
    public sealed partial class StackImplementationTests
    {
        public class LinkedListStackTests : QueueImplementationTester<LinkedListStack<int>>
        {
            protected override IStack<int> CreateSubject() => new LinkedListStack<int>();
        }
    }
}