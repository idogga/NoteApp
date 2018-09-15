using NoteAppModel;
using System.Linq;

namespace NoteAppService
{
    public delegate string CommandDelegate(string inRequest);

    public class RequestHelper
    {
        private CommandDelegate _func;

        public RequestHelper(Logger logger, string command)
        {
            var methods = new Methods(logger);
            var availableCommands = methods.GetAvailableCommands();
            _func = availableCommands.Keys.Contains(command.ToUpper()) ? availableCommands[command.ToUpper()] : null;
        }

        public bool IsContains()
        {
            return _func != null;
        }

        public string Execute(string param)
        {
            return _func(param);
        }
    }
}
