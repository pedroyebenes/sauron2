using System;
namespace Sauron2.Exceptions
{
    public class TimeException : Exception
    {
        public TimeException()
        {
        }

        public TimeException(string message) : base(message)
        {
        }
    }
}
