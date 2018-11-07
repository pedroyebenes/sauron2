using System;
namespace Sauron2.Exceptions
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
