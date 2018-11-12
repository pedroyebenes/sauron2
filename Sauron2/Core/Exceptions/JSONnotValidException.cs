using System;
namespace Sauron2.Core.Exceptions
{
    public class JSONnotValidException : Exception
    {
        public JSONnotValidException()
        {
        }

        public JSONnotValidException(string message) : base(message)
        {
        }
    }
}
