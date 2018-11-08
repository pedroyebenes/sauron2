using System;
using System.Collections.Generic;
using System.IO;
using System.Json;

using Sauron2.Core;

namespace Sauron2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hail SAURON!");
            SimulationEnvironment simEnv = new SimulationEnvironment("../../module1.json");

            simEnv.Init();
            simEnv.Run();
        }
    }
}
