using Princeton_Algorithms1.Week02.Queue;

namespace Test.Week02.Queue
{
    public sealed partial class QueueImplementationTests
    {
        public class ArrayQueueTests : QueueImplementationTester<ArrayQueue<int>>
        {
            protected override IQueue<int> CreateSubject() => new ArrayQueue<int>();
        }
    }
}