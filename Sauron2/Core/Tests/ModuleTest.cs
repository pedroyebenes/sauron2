using NUnit.Framework;
using System;

namespace Sauron2.Core.Tests
{
    public class MockModule : Module
    {
        public MockModule(string name, int gates) : base(name, gates) { }
        public MockModule(string jsonString) : base(jsonString) { }
        public override void Initialize() { }
        public override void HandleEvent(Event e) { }
        public override void Finish() { }
    }

    [TestFixture()]
    public class ModuleTest
    {
        [Test()]
        public void TestConstructor()
        {
            Module m = new MockModule("m", 2);
            Assert.True(m.Name == "m");
            Assert.True(m.Gate.Count == 2);
        }

        [Test()]
        public void TestJSONConstructor()
        {
            Module m = new MockModule(@"{ ""Gates"": 5, ""Name"": ""Sauron""}");
            Assert.True(m.Name == "Sauron");
            Assert.True(m.Gate.Count == 5);
        }

        [Test()]
        public void TestWrongJSONConstructor0()
        {
            bool exception = false;
            try
            {
                Module m = new MockModule(@"{ ""Gates"": -1, ""Name"": ""Sauron""}");
            }
            catch (ArgumentException)
            {
                exception = true;
            }
            Assert.True(exception);
        }

        [Test()]
        public void TestWrongJSONConstructor1()
        {
            bool exception = false;
            try
            {
                Module m = new MockModule(@"{ ""Gates"": 1, ""Name"": """"}");
            }
            catch (ArgumentException)
            {
                exception = true;
            }
            Assert.True(exception);
        }

    }
}
