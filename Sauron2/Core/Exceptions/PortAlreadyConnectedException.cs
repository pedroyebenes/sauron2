using System;
namespace Sauron2.Core.Exceptions
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
