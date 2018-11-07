using NUnit.Framework;
using System;
using System.Collections.Generic;

using Sauron2.Core;

namespace Sauron2.Tests
{
    [TestFixture]
    public class JSONParserTest
    {
        readonly string json = @"
{
    ""Modules"": [
        {
                ""Name"": ""Sauron"", 
            ""Gates"": 2
        },
        {
                ""Name"": ""Melkor"", 
            ""Gates"": 2
        },
        {
                ""Name"": ""Iluvatar"", 
            ""Gates"": 2
        }
    ],
    ""Connections"": [
        { 
            ""ModuleA"": ""Sauron"", ""IndexA"": 0, ""PortA"": 0,
            ""ModuleB"": ""Melkor"", ""IndexB"": 0, ""PortB"": 0
        },
        { 
            ""ModuleA"": ""Sauron"", ""IndexA"": 0, ""PortA"": 1, 
            ""ModuleB"": ""Iluvatar"", ""IndexB"": 0, ""PortB"": 0 
        },
        { 
            ""ModuleA"": ""Melkor"", ""IndexA"": 0, ""PortA"": 1, 
            ""ModuleB"": ""Iluvatar"", ""IndexB"": 0, ""PortB"": 1 
        }
    ]
}";
        JSONParser Jp;

        [SetUp]
        public void Init()
        {
            Jp = new JSONParser(json);
        }

        [Test]
        public void TestGetModules()
        {
            List<Module> ml = Jp.GetModules();

            Assert.True(ml[0].Name == "Sauron");
            Assert.True(ml[0].Gate.Count == 2);

            Assert.True(ml[1].Name == "Melkor");
            Assert.True(ml[1].Gate.Count == 2);

            Assert.True(ml[2].Name == "Iluvatar");
            Assert.True(ml[2].Gate.Count == 2);
        }

        [Test]
        public void TestGetConnections()
        {
            List<PreConnection> pcl = Jp.GetConnections();

            Assert.True(pcl[0].ModuleA == "Sauron");
            Assert.True(pcl[0].IndexA == 0);
            Assert.True(pcl[0].PortA == 0);
            Assert.True(pcl[0].ModuleB == "Melkor");
            Assert.True(pcl[0].IndexB == 0);
            Assert.True(pcl[0].PortB == 0);

            Assert.True(pcl[1].ModuleA == "Sauron");
            Assert.True(pcl[1].IndexA == 0);
            Assert.True(pcl[1].PortA == 1);
            Assert.True(pcl[1].ModuleB == "Iluvatar");
            Assert.True(pcl[1].IndexB == 0);
            Assert.True(pcl[1].PortB == 0);

            Assert.True(pcl[2].ModuleA == "Melkor");
            Assert.True(pcl[2].IndexA == 0);
            Assert.True(pcl[2].PortA == 1);
            Assert.True(pcl[2].ModuleB == "Iluvatar");
            Assert.True(pcl[2].IndexB == 0);
            Assert.True(pcl[2].PortB == 1);
        }
    }
}
