using NUnit.Framework;
using System;
using Sauron2.Core;

namespace Sauron2.Tests
{
    [TestFixture()]
    public class ModuleTest
    {
        [Test()]
        public void TestConstructor()
        {
            Module m = new Module("m", 2);
            Assert.True(m.Name == "m");
            Assert.True(m.Gate.Count == 2);
        }

        [Test()]
        public void TestJSONConstructor()
        {
            Module m = new Module(@"{ ""Gates"": 5, ""Name"": ""Sauron""}");

            Assert.True(m.Name == "Sauron");
            Assert.True(m.Gate.Count == 5);
        }
    }
}
