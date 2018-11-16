using System;

using Sauron2.Core;
using Sauron2.Core.UserInterfaces;
using Sauron2.Modules;

namespace Sauron2
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            ArgumentParser ap = new ArgumentParser(args);

            Environment.CurrentDirectory = ap.ConfigPath;
            SimulationEnvironment simEnv = new SimulationEnvironment(
                                                  ap.ConfigFileName,
                                                  new ModuleFactory(),
                                                  new CommnadLineInterface(ap.SilentMode));

            simEnv.Init();

            simEnv.Run();
        }
    }
}
