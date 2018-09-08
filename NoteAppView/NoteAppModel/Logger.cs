using System;
using System.IO;

namespace NoteAppModel
{
    public class Logger
    {
        public Logger()
        {

        }

        #region Публичные методы
        /// <summary>
        /// Запись в консоль
        /// </summary>
        /// <param name="str">строка для логгирования</param>
        public void Write(string str)
        {
            var log = GetLogString(str);
            System.Diagnostics.Debug.WriteLine(log);
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToShortDateString() + ".log", log);
        }

        /// <summary>
        /// Запись в консоль
        /// </summary>
        /// <param name="obj">Объект для логгирования</param>
        public void Write(object obj)
        {
            if (obj != null)
            {
                var log = GetLogString(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
                System.Diagnostics.Debug.WriteLine(log);
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToShortDateString() + ".log", log);
            }
        }
        #endregion

        #region Приватные методы

        /// <summary>
        /// Получение строки логгов
        /// </summary>
        /// <param name="str">Данные</param>
        /// <returns>Запись</returns>
        private string GetLogString(string str)
        {
            return $"LOG {DateTime.Now.ToLongTimeString()} : {str}";
        }

        #endregion
    }
}
