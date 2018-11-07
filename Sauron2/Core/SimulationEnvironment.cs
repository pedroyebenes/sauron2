using System;
using System.Collections.Generic;
using System.Json;

namespace Sauron2.Core
{
    public class SimulationEnvironment
    {
        public EventQueue EventQueue { get; private set; }
        public Dictionary<string, List<Module>> DictModules { get; }
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

        public void AddModules(List<Module> lm)
        {
            foreach (Module m in lm)
            {
                AddModule(m);
            }
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

        void ConnectModulesWithPreConnection(PreConnection pc)
        {
            Module mA = GetModule(pc.ModuleA, pc.IndexA);
            Module mB = GetModule(pc.ModuleB, pc.IndexB);

            Connection.Connect(mA, pc.PortA, mB, pc.PortB);
        }

        public void ConnectModules(List<PreConnection> pl)
        {
            foreach (PreConnection pc in pl)
            {
                ConnectModulesWithPreConnection(pc);
            }
        }

        public void Run()
        {
            //Initialize
            foreach(List<Module> lm in DictModules.Values)
            {
                foreach(Module m in lm)
                {
                    m.Initialize();
                }
            }

            while (EventQueue.IsNotEmpty())
            {
                Event e = EventQueue.GetNextEvent();
                Time = e.Time;
                e.DestModule.HandleEvent(e);
            }

            //Finish
            foreach (List<Module> lm in DictModules.Values)
            {
                foreach (Module m in lm)
                {
                    m.Finish();
                }
            }
        }
    }
}
