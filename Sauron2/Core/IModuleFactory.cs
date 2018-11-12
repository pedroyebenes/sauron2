namespace Sauron2.Core
{
    public interface IModuleFactory
    {
        Module CreateModule(string type, string jsonString);
    }
}