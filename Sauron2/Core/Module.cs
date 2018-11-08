using System;
using System.Collections.Generic;
using System.IO;
using System.Json;

namespace Sauron2.Core
{
    public abstract class Module
    {
        internal static ulong IDCount = 0;

        public SimulationEnvironment SimEnvir { get; set; }
        public ulong ID { get; private set; }
        public ulong Index { get; set; }
        public string Name { get; private set; }

        public List<Connection> Gate { get; set; }

        protected Module(string name, int gates)
        {
            ID = IDCount;
            IDCount += 1;

            Name = name;
            Gate = new List<Connection>(gates);
            for (int i = 0; i < gates; i++)
            {
                Gate.Add(null);
            }
        }

        protected Module(string jsonString)
        {
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);
            if (jo.TryGetValue(nameof(Name), out JsonValue value))
            {
                Name = (string)value;
            }
            if (jo.TryGetValue("Gates", out value))
            {
                Gate = new List<Connection>(value);
                for (int i = 0; i < value; i++)
                {
                    Gate.Add(null);
                }
            }
            Console.WriteLine("Name: {0}, Gates: {1}", Name, Gate.Count);
        }

        public void Send(Event e, int port, ulong time)
        {
            e.Time = time;
            e.SrcModule = this;
            e.SrcPort = port;
            e.DestModule = Gate[port].Module;
            e.DestPort = Gate[port].Port;

            SimEnvir.EventQueue.Add(e);
        }

        public void ScheduleAt(Event e, ulong time)
        {
            e.Time = time;
            e.SrcModule = this;
            e.SrcPort = -1;
            e.DestModule = this;
            e.DestPort = -1;

            SimEnvir.EventQueue.Add(e);
        }

        abstract public void Initialize();
        abstract public void HandleEvent(Event e);
        abstract public void Finish();
    }
}
