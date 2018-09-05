using System;

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
            System.Diagnostics.Debug.WriteLine(GetLogString(str));
        }

        /// <summary>
        /// Запись в консоль
        /// </summary>
        /// <param name="obj">Объект для логгирования</param>
        public void Write(object obj)
        {
            if (obj != null)
            {
                System.Diagnostics.Debug.WriteLine(GetLogString(Newtonsoft.Json.JsonConvert.SerializeObject(obj)));
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
