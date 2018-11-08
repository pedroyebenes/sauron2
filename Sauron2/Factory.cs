using System;
using Sauron2.Core;
using Sauron2.Exceptions;
namespace Sauron2
{
    public enum ModulesEnumeration
    {
        Node
    }
    public static class Factory
    {
        public static Module GetModule(string type, string jsonString)
        {
            Module m = null;
            if (type == nameof(Node))
            {
                m = new Node(jsonString);
            }
            else
            {
                throw new UnknownModuleTypeException("Module type '" + type + "' is not defined");
            }
            return m;
        }
    }
}
