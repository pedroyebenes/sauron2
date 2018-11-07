using NUnit.Framework;
using System;
namespace Sauron2.Tests
{
    [TestFixture()]
    public class SimulationEnvironmentTest
    {
        [Test()]
        public void TestCase()
        {
            SimulationEnvironment simEnv = new SimulationEnvironment();
            simEnv.AddModule(new Module("m", 2));
            simEnv.AddModule(new Module("m", 2));

            Module a = simEnv.GetModule("m", 0);
            Assert.NotNull(a);
            Module b = simEnv.GetModule("m", 1);
            Assert.NotNull(b);
            Connection.Connect(a, 0, b, 1);

            Event e = new Event();
            a.Send(e, 0, 10);

            simEnv.Run();
        }
    }
}
