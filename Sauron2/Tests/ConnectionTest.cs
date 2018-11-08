using NUnit.Framework;
using System;
using Sauron2.Core;
using Sauron2.Exceptions;

namespace Sauron2.Tests
{
    [TestFixture()]
    public class ConnectionTest
    {
        [Test()]
        public void TestConnection()
        {
            Module ma = new MockModule("ma", 2);
            Module mb = new MockModule("mb", 2);

            Connection.Connect(ma, 0, mb, 1);

            Assert.True(ma.Gate[0].Module == mb);
            Assert.True(ma.Gate[0].Port == 1);
            Assert.True(mb.Gate[1].Module == ma);
            Assert.True(mb.Gate[1].Port == 0);
        }

        [Test()]
        public void TestDoubleConnection()
        {
            Module ma = new MockModule("ma", 2);
            Module mb = new MockModule("mb", 2);

            bool exception = false;
            try{
                Connection.Connect(ma, 0, mb, 1);
                Connection.Connect(ma, 0, mb, 1);
            }
            catch (PortAlreadyConnectedException)
            {
                exception = true; 
            }
            Assert.True(exception);
        }
    }
}