using NoteAppModel;
using System.Linq;

namespace NoteAppService
{
    /// <summary>
    /// Функция комманды
    /// </summary>
    /// <param name="inRequest">входные данные</param>
    /// <returns>Выходные данные</returns>
    public delegate string CommandDelegate(string inRequest);

    /// <summary>
    /// Контроллер запросов
    /// </summary>
    public class RequestHelper
    {
        private CommandDelegate _func;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="command">Название комманды</param>
        public RequestHelper(string command)
        {
            var methods = new Methods();
            var availableCommands = methods.GetAvailableCommands();
            _func = availableCommands.Keys.Contains(command.ToUpper()) ? availableCommands[command.ToUpper()] : null;
        }

        /// <summary>
        /// Имеется ли такая функция
        /// </summary>
        /// <returns>Результат</returns>
        public bool IsContains()
        {
            return _func != null;
        }

        /// <summary>
        /// Выполнение комманды
        /// </summary>
        /// <param name="param">Входные данные</param>
        /// <returns>Результат выполнения</returns>
        public string Execute(string param)
        {
            return _func(param);
        }
    }
}
