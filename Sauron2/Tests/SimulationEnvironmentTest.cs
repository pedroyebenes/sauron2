using NUnit.Framework;
using System;

using Sauron2.Core;

namespace Sauron2.Tests
{
    [TestFixture()]
    public class SimulationEnvironmentTest
    {
        [Test()]
        public void TestAddGetModules()
        {
            SimulationEnvironment simEnv = new SimulationEnvironment("");
            simEnv.AddModule(new MockModule("m", 2));
            simEnv.AddModule(new MockModule("m", 2)); //Index 1

            Module a = simEnv.GetModule("m", 0);
            Assert.NotNull(a);
            Module b = simEnv.GetModule("m", 1);
            Assert.NotNull(b);
        }
    }
}
