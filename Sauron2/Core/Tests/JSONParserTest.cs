using NUnit.Framework;
using System.Collections.Generic;
using Sauron2.Modules;

namespace Sauron2.Core.Tests
{
    [TestFixture]
    public class JSONParserTest
    {
        readonly string json1 = @"
{
    ""Modules"": [
        {
            ""Name"": ""Sauron"", 
            ""Type"": ""Node"", 
            ""Gates"": 2
        },
        {
            ""Name"": ""Melkor"", 
            ""Type"": ""Node"", 
            ""Gates"": 2
        },
        {
            ""Name"": ""Iluvatar"", 
            ""Type"": ""Node"", 
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
        readonly string json2 = @"
{
    ""Modules"": [
        {
            ""Name"": ""Sauron"", 
            ""Type"": ""Node"", 
            ""Number"": 3,
            ""Gates"": 2
        }
    ],
    ""Connections"": [
        { 
            ""ModuleA"": ""Sauron"", ""IndexA"": 0, ""PortA"": 0,
            ""ModuleB"": ""Sauron"", ""IndexB"": 1, ""PortB"": 0
        },
        { 
            ""ModuleA"": ""Sauron"", ""IndexA"": 0, ""PortA"": 1, 
            ""ModuleB"": ""Sauron"", ""IndexB"": 2, ""PortB"": 0 
        },
        { 
            ""ModuleA"": ""Sauron"", ""IndexA"": 1, ""PortA"": 1, 
            ""ModuleB"": ""Sauron"", ""IndexB"": 2, ""PortB"": 1 
        }
    ]
}";

        [Test]
        public void TestGetModules1()
        {
            JSONParser Jp = new JSONParser(json1);
            List<Module> ml = Jp.GetModules(new ModuleFactory());

            Assert.True(ml[0].Name == "Sauron");
            Assert.True(ml[0].Gate.Count == 2);

            Assert.True(ml[1].Name == "Melkor");
            Assert.True(ml[1].Gate.Count == 2);

            Assert.True(ml[2].Name == "Iluvatar");
            Assert.True(ml[2].Gate.Count == 2);
        }

        [Test]
        public void TestGetConnections1()
        {
            JSONParser Jp = new JSONParser(json1);
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

        [Test]
        public void TestGetModules2()
        {
            JSONParser Jp = new JSONParser(json2);
            List<Module> ml = Jp.GetModules(new ModuleFactory());

            Assert.True(ml[0].Name == "Sauron");
            Assert.True(ml[0].Gate.Count == 2);

            Assert.True(ml[1].Name == "Sauron");
            Assert.True(ml[1].Gate.Count == 2);

            Assert.True(ml[2].Name == "Sauron");
            Assert.True(ml[2].Gate.Count == 2);
        }

        [Test]
        public void TestGetConnections2()
        {
            JSONParser Jp = new JSONParser(json2);
            List<PreConnection> pcl = Jp.GetConnections();

            Assert.True(pcl[0].ModuleA == "Sauron");
            Assert.True(pcl[0].IndexA == 0);
            Assert.True(pcl[0].PortA == 0);
            Assert.True(pcl[0].ModuleB == "Sauron");
            Assert.True(pcl[0].IndexB == 1);
            Assert.True(pcl[0].PortB == 0);

            Assert.True(pcl[1].ModuleA == "Sauron");
            Assert.True(pcl[1].IndexA == 0);
            Assert.True(pcl[1].PortA == 1);
            Assert.True(pcl[1].ModuleB == "Sauron");
            Assert.True(pcl[1].IndexB == 2);
            Assert.True(pcl[1].PortB == 0);

            Assert.True(pcl[2].ModuleA == "Sauron");
            Assert.True(pcl[2].IndexA == 1);
            Assert.True(pcl[2].PortA == 1);
            Assert.True(pcl[2].ModuleB == "Sauron");
            Assert.True(pcl[2].IndexB == 2);
            Assert.True(pcl[2].PortB == 1);
        }
    }
}
