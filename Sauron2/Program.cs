using System;

namespace Sauron2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            EventQueue eventQueue = new EventQueue();

            eventQueue.Add(new Event(0));
            eventQueue.Add(new Event(1));
            eventQueue.Add(new Event(1));
            eventQueue.Add(new Event(5));
            eventQueue.Add(new Event(1));
            eventQueue.Add(new Event(2));
            eventQueue.Add(new Event(5));

            while (eventQueue.IsNotEmpty())
            {
                Event e = eventQueue.GetNextEvent();
                Console.WriteLine("Event " + e.GetID() + " Time: " + e.GetTime());
            }
            eventQueue.Add(new Event(2));
        }
    }
}
