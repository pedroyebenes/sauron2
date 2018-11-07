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
            SimulationEnvironment simEnv = new SimulationEnvironment();

            JSONParser jp = JSONParser.WithFilename("../../module.json");
            List<Module> ml = jp.GetModules();
            simEnv.AddModules(ml);

            List<PreConnection> pl = jp.GetConnections();
            simEnv.ConnectModules(pl);

            ml[0].Send(new Event(), 0, 0);
            ml[0].Send(new Event(), 1, 0);
            ml[1].Send(new Event(), 1, 0);

            simEnv.Run();
        }
    }
}
