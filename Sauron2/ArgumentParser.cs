using System;
using System.IO;

namespace Sauron2
{
    public class ArgumentParser
    {
        readonly string[] Args;
        public string ConfigPath { private set; get; }
        public string ConfigFileName { private set; get; }
        public bool SilentMode { private set; get; }

        public ArgumentParser(string[] args)
        {
            Args = args;
            SetDefaultValues();

            CheckForHelp();
            ParseArguments();
        }

        void SetDefaultValues()
        {
            ConfigPath = "../../Configurations/ThreeNodes";
            ConfigFileName = "config.json";
            SilentMode = false;
        }

        void CheckForHelp()
        {
            foreach (string s in Args)
            {
                if (s == "-h" || s == "--help")
                {
                    Console.WriteLine("Usage: Sauron.exe -c=ConfigurationFile.json [-s]"); //TODO
                    System.Environment.Exit(0);
                }
            }
        }

        bool ParseArgument(string arg, string name, out string result)
        {
            bool parsed = false;
            result = "";
            string[] s = arg.Split('=');

            if (s.Length == 2)
            {
                if (s[0] == name)
                {
                        result = s[1];
                        parsed = true;
                }
            }
            return parsed;
        }

        void ParseArguments()
        {
            foreach (string s in Args)
            {
                if (ParseArgument(s, "-c", out string value))
                {
                    ConfigPath = Path.GetDirectoryName(value);
                    ConfigFileName = Path.GetFileName(value);
                }
                else if (s == "-s")
                {
                    SilentMode = true;
                }
            }
        }
    }
}
