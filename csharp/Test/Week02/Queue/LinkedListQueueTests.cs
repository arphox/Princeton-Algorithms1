using Princeton_Algorithms1.Week02.Queue;

namespace Test.Week02.Queue
{
    public sealed class LinkedListQueueTests : QueueImplementationTester<LinkedListQueue<int>>
    {
        protected override IQueue<int> CreateSubject() => new LinkedListQueue<int>();
    }
}