
namespace Nintenlord.Event_Assembler.Core.IO.Logs
{
    public interface ILog
    {
        void AddError(string message);
        void AddError(string format, params object[] parameters);
        void AddError(string file, string line, string message);

        void AddWarning(string message);
        void AddWarning(string format, params object[] parameters);
        void AddWarning(string file, string line, string message);

        void AddMessage(string message);
        void AddMessage(string format, params object[] parameters);
        void AddMessage(string file, string line, string message);

        void Clear();
        void PrintAll();
    }
}
