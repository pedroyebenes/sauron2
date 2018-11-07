using System;
using System.Collections.Generic;
using System.Json;

namespace Sauron2
{
    public class SimulationEnvironment
    {
        public EventQueue EventQueue { get; private set; }
        private readonly Dictionary<string, List<Module>> DictModules;
        public ulong Time { get; private set; }

        public SimulationEnvironment()
        {
            EventQueue = new EventQueue();
            DictModules = new Dictionary<string, List<Module>>();
            Time = 0;
        }

        public void AddModule(Module module)
        {
            module.SimEnvir = this;

            if (!DictModules.ContainsKey(module.Name))
            {
                DictModules[module.Name] = new List<Module>();
            }

            module.Index = (ulong)DictModules[module.Name].Count;
            DictModules[module.Name].Add(module);
        }

        public Module GetModule(string name, int index)
        {
            Module m = null;
            if (DictModules.ContainsKey(name))
            {
                if (DictModules[name].Count > index)
                {
                    m = DictModules[name][index];
                }
            }
            return m;
        }

        public void Run()
        {
            while (EventQueue.IsNotEmpty())
            {
                Event e = EventQueue.GetNextEvent();
                Time = e.Time;
                e.DestModule.HandleEvent(e);
            }
        }
    }
}
