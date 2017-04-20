using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class TestClass
    {
        private Task.Queue<string> queue;

        public void InitQueue()
        {
                queue = new Task.Queue<string>();
                queue.Enqueue("Lorem");
                queue.Enqueue("ipsum");
                queue.Enqueue("dolor");
                queue.Enqueue("sit");
                queue.Enqueue("amet");
        }

        [Test]
        public void Enumerator_Test()
        {
                InitQueue();
                var enumerator = queue.GetEnumerator();
                enumerator.MoveNext();
                Assert.AreEqual("Lorem", enumerator.Current);
                enumerator.MoveNext();
                Assert.AreEqual("ipsum", enumerator.Current);
                enumerator.MoveNext();
                Assert.AreEqual("dolor", enumerator.Current);
        }

        [Test]
        public void Dequeue_Test()
        {
            InitQueue();
            Assert.AreEqual("Lorem", queue.Dequeue());
            Assert.AreEqual("ipsum", queue.Dequeue());
            Assert.AreEqual("dolor", queue.Dequeue());
        }
    }
}
