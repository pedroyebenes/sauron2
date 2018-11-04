using NUnit.Framework;
using System;
namespace Sauron2.Tests
{
    [TestFixture()]
    public class EventQueueTest
    {
        [Test()]
        public void TestCase()
        {
            EventQueue eventQueue = new EventQueue();
            eventQueue.Add(new Event(0));
            eventQueue.Add(new Event(1));
            eventQueue.Add(new Event(1));
            eventQueue.Add(new Event(5));
            eventQueue.Add(new Event(1));
            eventQueue.Add(new Event(2));
            eventQueue.Add(new Event(5));

            ulong time = 0;
            while (eventQueue.IsNotEmpty())
            {
                Event e = eventQueue.GetNextEvent();
                Assert.GreaterOrEqual(e.GetTime(), time);
                time = e.GetTime();
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

                eventQueue.Add(new Event((ulong)randomGenerator.Next(0, 100)));
            }

            ulong time = 0;
            while (eventQueue.IsNotEmpty())
            {
                Event e = eventQueue.GetNextEvent();
                Assert.GreaterOrEqual(e.GetTime(), time);
                time = e.GetTime();
            }
            Assert.False(eventQueue.IsNotEmpty());
        }
    }
}
