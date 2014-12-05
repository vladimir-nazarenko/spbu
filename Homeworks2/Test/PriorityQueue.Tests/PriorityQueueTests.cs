using System;
using NUnit.Framework;
using PQueue;

namespace PQueue.Tests
{
    [TestFixture()]
    public class PriorityQueueTest
    {
        [SetUp]
        public void CreateList()
        {
            this.queue = new PriorityQueue<string>();
        }

        [Test]
        public void Enqueue_SeveralValues_Success()
        {
            queue.Enqueue("highPriority", 1000000);
            queue.Enqueue("averangePriority", 1000);
            queue.Enqueue("lowPriority", 1);
        }

        [ExpectedException(typeof(EmptyQueueException))]
        [Test]
        public void Dequeue_FromEmptyQueue_ExceptionThrown()
        {
            queue.Dequeue();
        }

        [Test]
        public void EnqueueDequeueInteraction_BunchOfCallsBoth_Success()
        {
            queue.Enqueue("averangePriority", 1000);
            queue.Enqueue("highPriority", 1000000);
            queue.Enqueue("lowPriority", 1);
            Assert.AreEqual("highPriority", queue.Dequeue());
            Assert.AreEqual("averangePriority", queue.Dequeue());
            Assert.AreEqual("lowPriority", queue.Dequeue());
        }

        [Test]
        public void Dequeue_EqualPriority_Success()
        {
            queue.Enqueue("first", 1);
            queue.Enqueue("second", 1);
            queue.Enqueue("third", 1);
            Assert.AreEqual("first", queue.Dequeue());
            Assert.AreEqual("second", queue.Dequeue());
            Assert.AreEqual("third", queue.Dequeue());
        }

        private PriorityQueue<string> queue;
    }
}
