using System;
namespace Sauron2.Exceptions
{
    public class PortAlreadyConnectedException : Exception
    {
        public PortAlreadyConnectedException()
        {
        }

        public PortAlreadyConnectedException(string message) : base(message)
        {
        }
    }
}
