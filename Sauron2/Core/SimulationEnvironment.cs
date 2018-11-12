using System.Collections.Generic;
using Sauron2.Core.UserInterfaces;

namespace Sauron2.Core
{
    public class SimulationEnvironment
    {
        public IUserInterface UI { get; private set; }
        public IModuleFactory ModuleFactory { get; private set; }

        public SimulationParameters Parameters { get; private set; }

        readonly EventQueue EventQueue;
        public Dictionary<string, List<Module>> DictModules { get; }

        public ulong Time { get; private set; }

        readonly string ConfigurationFileName;

        public SimulationEnvironment(string configFilename, IModuleFactory moduleFactory, IUserInterface userInterface)
        {
            EventQueue = new EventQueue();
            DictModules = new Dictionary<string, List<Module>>();
            UI = userInterface;
            ModuleFactory = moduleFactory;

            Time = 0;
            ConfigurationFileName = configFilename;
        }

        public void AddEvent(Event e)
        {
            EventQueue.Add(e);
        }

        public void Init()
        {
            JSONParser jp = new JSONParser(JSONParser.ReadJSONFile(ConfigurationFileName));

            Parameters = jp.GetParameters();
            List<Module> ml = jp.GetModules(ModuleFactory);
            List<PreConnection> pl = jp.GetConnections();

            AddModules(ml);
            ConnectModules(pl);

            //Initialize modules
            foreach (List<Module> lm in DictModules.Values)
            {
                foreach (Module m in lm)
                {
                    m.Initialize();
                }
            }
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

            while (EventQueue.IsNotEmpty() && Time < Parameters.TimeLimit)
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
