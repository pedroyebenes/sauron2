using System;
using Sauron2.Core;
namespace Sauron2.Modules
{
    public enum ModuleEnum
    {
        Node
    }
    public class ModuleFactory : IModuleFactory
    {
        public ModuleEnum FromStringToType(string s)
        {
            ModuleEnum moduleEnum;

            if (s == nameof(Node)) moduleEnum = ModuleEnum.Node;
            else throw new ArgumentException("Event type '" + s + "' is not defined");

            return moduleEnum;
        }

        public Module CreateModule(string type, string jsonString)
        {
            Module m = null;
            if (type == nameof(Node))
            {
                m = new Node(jsonString);
            }
            else
            {
                throw new ArgumentException("Module type '" + type + "' is not defined");
            }
            return m;
        }
    }
}
