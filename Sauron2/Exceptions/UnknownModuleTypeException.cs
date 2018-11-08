using System;
namespace Sauron2.Exceptions
{
    public class UnknownModuleTypeException : Exception
    {
        public UnknownModuleTypeException()
        {
        }

        public UnknownModuleTypeException(string message) : base(message)
        {
        }
    }
}
