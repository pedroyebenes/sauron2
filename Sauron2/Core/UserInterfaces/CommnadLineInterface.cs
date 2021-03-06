﻿using System;
using System.IO;

namespace Sauron2.Core.UserInterfaces
{
    public class CommnadLineInterface : IUserInterface
    {
        TextWriter ErrorWriter = Console.Error;
        readonly Boolean Silent;

        public CommnadLineInterface(bool silent = false)
        {
            ErrorWriter = Console.Error;
            Silent = silent;
        }
        public void Error(string s)
        {
            ErrorWriter.WriteLine("ERROR: " + s);
        }

        public void Show(string s)
        {
            if(!Silent) Console.WriteLine(s);
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
