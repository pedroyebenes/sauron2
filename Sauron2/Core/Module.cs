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

        void SetUp(string name, int gates)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is null");
            }
            if(gates < 0)
            {
                throw new ArgumentException("The number of gates is < 0");
            }

            ID = IDCount;
            IDCount += 1;

            Name = name;
            Gate = new List<Connection>(gates);
            for (int i = 0; i < gates; i++)
            {
                Gate.Add(null);
            }
        }

        protected Module(string name, int gates)
        {
            SetUp(name, gates);
        }

        protected Module(string jsonString)
        {
            string name = "";
            int gates = -1;
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);
            if (jo.TryGetValue(nameof(Name), out JsonValue value))
            {
                name = (string)value;
            }
            if (jo.TryGetValue("Gates", out value))
            {
                gates = (int)value;
            }
            Console.WriteLine(jsonString);
            SetUp(name, gates);
        }

        void UpdateEvent(Event e, ulong time, Module srcModule, int srcPort, Module destModule, int destPort)
        {
            e.Time = time;
            e.SrcModule = srcModule;
            e.SrcPort = srcPort;
            e.DestModule = destModule;
            e.DestPort = destPort;
        }

        public void Send(Event e, int port, ulong time)
        {
            UpdateEvent(e, time, this, port, Gate[port].Module, Gate[port].Port);
            SimEnvir.AddEvent(e);
        }

        public void ScheduleAt(Event e, ulong time)
        {
            UpdateEvent(e, time, this, -1, this, -1);
            SimEnvir.AddEvent(e);
        }

        abstract public void Initialize();
        abstract public void HandleEvent(Event e);
        abstract public void Finish();
    }
}
