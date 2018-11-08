﻿using System;
using Sauron2.Core;

namespace Sauron2
{
    public class Node : Module
    {
        public int Counter;
        private static readonly Random GetRandom = new Random(0);

        public Node(string name, int gates) : base(name, gates)
        {
        }
        public Node(string jsonString) : base(jsonString)
        {
        }

        public override void Initialize()
        {
            Counter = 0;
            if (ID == 0)
            {
                Send(new Event(), GetRandom.Next(0, Gate.Count), SimEnvir.Time);
            }
            Console.WriteLine("Initializing Module {0}_{1}", Name, Index);
        }

        public override void HandleEvent(Event e)
        {
            Console.WriteLine("T={0} Module {1}_{2}, Port {3} -- Handling Event(id {4}, time {5})", SimEnvir.Time, Name, Index, e.DestPort, e.ID, e.Time);
            if (SimEnvir.Time < 100)
            {
                Random r = new Random();
                int port = GetRandom.Next(0, Gate.Count);
                Send(e, port, SimEnvir.Time + 1);
                Counter += 1;
            }
        }

        public override void Finish()
        {
            Console.WriteLine("Finishing Module {0}_{1}: Counter {2}", Name, Index, Counter);
        }
    }
}