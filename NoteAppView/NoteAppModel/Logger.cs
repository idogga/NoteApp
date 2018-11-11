using System;
using System.IO;

namespace NoteAppModel
{
    public class Logger
    {
        object _obj = new object();
        private static Logger _instance;

        public static Logger GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
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
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", log + Environment.NewLine);
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
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", log + Environment.NewLine);
            }
        }

        /// <summary>
        /// Запись ошибки
        /// </summary>
        /// <param name="ex"></param>
        public void Write(Exception ex)
        {
            lock (_obj)
            {
                var log = GetLogString("Ошибка : " + ex.Message);
                System.Diagnostics.Debug.WriteLine(log);
                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "Errors-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", log + Environment.NewLine);
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
            return $"LOG {DateTime.Now.ToString("HH:mm:ss:fff")} : {str}";
        }

        #endregion
    }
}
