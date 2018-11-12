using System;
using System.IO;

namespace Sauron2.Core.UserInterfaces
{
    public class CommnadLineInterface : IUserInterface
    {
        TextWriter ErrorWriter = Console.Error;

        public CommnadLineInterface()
        {
            ErrorWriter = Console.Error;
        }
        public void Error(string s)
        {
            //Console.WriteLine("ERROR: " + s);
            ErrorWriter.WriteLine("ERROR: " + s);
        }

        public void Show(string s)
        {
            Console.WriteLine(s);
        }

        public void Warning(string s)
        {
            Console.WriteLine("WARNING: "+ s);
        }
        public void Exit(int code)
        {
            System.Environment.Exit(code);
        }
    }
}
