using System;
namespace Sauron2
{
    public class Connection
    {
        public Module Module { get; private set; }
        public int Port { get; private set; }

        public static void Connect(Module mA, int portA, Module mB, int portB)
        {
            if (mA.Gate[portA] != null)
            {
                throw new Exceptions.PortAlreadyConnectedException(""); //TODO fill the string
            }
            if (mB.Gate[portB] != null)
            {
                throw new Exceptions.PortAlreadyConnectedException("");
            }
            mA.Gate[portA] = new Connection(mB, portB);
            mB.Gate[portB] = new Connection(mA, portA);
        }

        private Connection(Module module, int port)
        {
            this.Module = module;
            this.Port = port;
        }
    }
}
