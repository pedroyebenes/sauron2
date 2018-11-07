using NUnit.Framework;
using System;
using Sauron2.Core;

namespace Sauron2.Tests
{
    [TestFixture()]
    public class ConnectionTest
    {
        [Test()]
        public void TestConnection()
        {

            Module ma = new Module("ma", 2);
            Module mb = new Module("mb", 2);

            Connection.Connect(ma, 0, mb, 1);

            Assert.True(ma.Gate[0].Module == mb);
            Assert.True(ma.Gate[0].Port == 1);
            Assert.True(mb.Gate[1].Module == ma);
            Assert.True(mb.Gate[1].Port == 0);
        }
    }
}