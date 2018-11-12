using NUnit.Framework;
using Sauron2.Core;
using System;
namespace Sauron2.Modules.Tests
{
    [TestFixture()]
    public class FactoryTest
    {
        [Test()]
        public void TestFactoryWrongtype()
        {
            bool exception = false;
            try
            {
                ModuleFactory mF = new ModuleFactory();
                Module m = mF.CreateModule("", @"{ ""Gates"": 5, ""Name"": ""Sauron"", ""Type"": """"}");
            }
            catch (ArgumentException)
            {
                exception = true;
            }
            Assert.True(exception);
        }

        [Test()]
        public void TestFactoryNode()
        {
            ModuleFactory mF = new ModuleFactory();
            Module m = mF.CreateModule(nameof(Node), @"{ ""Gates"": 5, ""Name"": ""Sauron"", ""Type"": ""Node""}");
            Assert.True(m.GetType().Name == nameof(Node));
            Assert.True(m.Name == "Sauron");
            Assert.True(m.Gate.Count == 5);
        }
    }
}
