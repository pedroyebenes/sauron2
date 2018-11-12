using System;
namespace Sauron2.Core.UserInterfaces
{
    public interface IUserInterface
    {
        void Show(string s);
        void Warning(string s);
        void Error(string s);
        void Exit(int code);
    }
}
