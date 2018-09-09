using System;
using System.IO;
using System.Text;

namespace NoteAppModel
{
    public class Logger
    {
        object _obj = new object();

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
            lock (_obj)
            {
                var log = GetLogString(str);
                System.Diagnostics.Debug.WriteLine(log);
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToShortDateString() + ".log", log + Environment.NewLine);
            }
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
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToShortDateString() + ".log", log + Environment.NewLine);
            }
        }

        public void Write(Exception ex)
        {
            lock (_obj)
            {
                var log = GetLogString("Ошибка : " + ex.Message+Environment.NewLine+ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(log);
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Ошибки-" + DateTime.Now.ToShortDateString() + ".log", log + Environment.NewLine);
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
