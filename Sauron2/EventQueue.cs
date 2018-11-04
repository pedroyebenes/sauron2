using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Sauron2
{
    public class EventQueue
    {
        private readonly SortedSet<ulong> Times;
        private readonly Dictionary<ulong, Queue<Event>> DictQueues;
        private ulong Count;
        private ulong LastTime;

        public EventQueue()
        {
            Times = new SortedSet<ulong>();
            DictQueues = new Dictionary<ulong, Queue<Event>>();
            Count = 0;
            LastTime = ulong.MinValue;
        }

        public bool IsNotEmpty()
        {
            return Count != 0;
        }

        public void Add(Event e)
        {
            ulong time = e.GetTime();
            if (time < LastTime)
            {
                throw new TimeException("Event time " + time + "< lastTime " + LastTime);
            }

            if (DictQueues.ContainsKey(time))
            {
                DictQueues[time].Enqueue(e);
            }
            else
            {
                Times.Add(time);
                Queue<Event> q = new Queue<Event>();
                q.Enqueue(e);
                DictQueues.Add(time, q);
            }
            Count += 1;
        }

        public Event GetNextEvent()
        {
            ulong time = Times.Min;
            Queue<Event> q = DictQueues[time];

            while (q.Count == 0)
            { // Empty queue, get next time
                Times.Remove(time);
                DictQueues.Remove(time);
                time = Times.Min;
                q = DictQueues[time];
            }

            LastTime = time;
            Count -= 1;
            return q.Dequeue();
        }
    }
}
