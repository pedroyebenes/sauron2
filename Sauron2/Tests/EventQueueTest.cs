using NUnit.Framework;
using System;
using Sauron2.Core;

namespace Sauron2.Tests
{
    [TestFixture()]
    public class EventQueueTest
    {
        [Test()]
        public void TestCase()
        {
            EventQueue eventQueue = new EventQueue();
            eventQueue.Add(new Event(0, null));
            eventQueue.Add(new Event(1, null));
            eventQueue.Add(new Event(1, null));
            eventQueue.Add(new Event(5, null));
            eventQueue.Add(new Event(1, null));
            eventQueue.Add(new Event(2, null));
            eventQueue.Add(new Event(5, null));

            ulong time = 0;
            while (eventQueue.IsNotEmpty())
            {
                Event e = eventQueue.GetNextEvent();
                Assert.GreaterOrEqual(e.Time, time);
                time = e.Time;
            }
            Assert.False(eventQueue.IsNotEmpty());
        }

        [Test()]
        [Repeat(100)]
        public void TestCaseRandom()
        {
            Random randomGenerator = new Random();
            EventQueue eventQueue = new EventQueue();
            for (int i = 0; i < 1000; i++)
            {
                eventQueue.Add(new Event((ulong)randomGenerator.Next(0, 100), null));
            }

            ulong time = 0;
            while (eventQueue.IsNotEmpty())
            {
                Event e = eventQueue.GetNextEvent();
                Assert.GreaterOrEqual(e.Time, time);
                time = e.Time;
            }
            Assert.False(eventQueue.IsNotEmpty());
        }
    }
}
