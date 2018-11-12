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
            //Console.WriteLine("Hail SAURON!");
            ArgumentParser ap = new ArgumentParser(args);

            SimulationEnvironment simEnv = new SimulationEnvironment(
                ap.ConfigFileName,
                new ModuleFactory(),
                new CommnadLineInterface(ap.SilentMode));

            simEnv.Init();

            simEnv.Run();
        }
    }
}
