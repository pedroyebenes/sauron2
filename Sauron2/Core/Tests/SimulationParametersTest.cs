using NUnit.Framework;
using System;
namespace Sauron2.Core.Tests
{
    [TestFixture()]
    public class SimulationParametersTest
    {
        [Test()]
        public void TestTimeLimit()
        {
            SimulationParameters sp = new SimulationParameters(@"{""time_limit"" : 99}");
            Assert.True(sp.TimeLimit == 99);
            sp = new SimulationParameters(@"{}");
            Assert.True(sp.TimeLimit == ulong.MaxValue);
        }
    }
}
