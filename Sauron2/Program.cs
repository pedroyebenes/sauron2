﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Json;

namespace Sauron2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hail SAURON!");
            SimulationEnvironment simEnv = new SimulationEnvironment();

            simEnv.AddModule(new Module("m", 2));
            simEnv.AddModule(new Module("m", 2));

            Module a = simEnv.GetModule("m", 0);
            Module b = simEnv.GetModule("m", 1);
            Connection.Connect(a, 0, b, 1);

            Event e = new Event();
            a.Send(e, 0, 10);

            simEnv.Run();
        }
    }
}
