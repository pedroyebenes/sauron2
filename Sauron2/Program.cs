using System;
using System.Collections.Generic;
using System.IO;
using System.Json;

using Sauron2.Core;
using Sauron2.Modules;

namespace Sauron2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hail SAURON!");

            SimulationEnvironment simEnv = new SimulationEnvironment("../../config.json", new ModuleFactory());

            simEnv.Init();
            simEnv.Run();
        }
    }
}
