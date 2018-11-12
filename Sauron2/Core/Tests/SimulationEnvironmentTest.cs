using NUnit.Framework;

namespace Sauron2.Core.Tests
{
    [TestFixture()]
    public class SimulationEnvironmentTest
    {
        [Test()]
        public void TestAddGetModules()
        {
            SimulationEnvironment simEnv = new SimulationEnvironment("", null);
            simEnv.AddModule(new MockModule("m", 2));
            simEnv.AddModule(new MockModule("m", 2)); //Index 1

            Module a = simEnv.GetModule("m", 0);
            Assert.NotNull(a);
            Module b = simEnv.GetModule("m", 1);
            Assert.NotNull(b);
        }
    }
}
