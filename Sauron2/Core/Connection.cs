using System;
namespace Sauron2.Core
{
    public class PreConnection
    {
        public string ModuleA { get; private set; }
        public string ModuleB { get; private set; }
        public int IndexA { get; private set; }
        public int IndexB { get; private set; }
        public int PortA { get; private set; }
        public int PortB { get; private set; }

        public PreConnection(string ma, int ia, int pa, string mb, int ib, int pb)
        {
            ModuleA = ma;
            ModuleB = mb;
            PortA = pa;
            PortB = pb;
            IndexA = ia;
            IndexB = ib;
        }
    }

    public class Connection
    {
        public Module Module { get; private set; }
        public int Port { get; private set; }

        public static void Connect(Module mA, int portA, Module mB, int portB)
        {
            if (mA.Gate[portA] != null)
            {
                throw new Exceptions.PortAlreadyConnectedException("Port " + portA + " in module " + mA.Name + "("+ mA.Index + ") not connected"); 
            }
            if (mB.Gate[portB] != null)
            {
                throw new Exceptions.PortAlreadyConnectedException("Port " + portB + " in module " + mB.Name + "(" + mB.Index + ") not connected");
            }
            mA.Gate[portA] = new Connection(mB, portB);
            mB.Gate[portB] = new Connection(mA, portA);
        }

        public Connection(Module module, int port)
        {
            this.Module = module;
            this.Port = port;
        }
    }
}
