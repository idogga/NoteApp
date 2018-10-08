using System;
using System.IO;

namespace NoteAppView.Controllers
{
    public class FileController
    {
        private string _domain;
        public FileController()
        {
            _domain = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// Сохранение объекта
        /// </summary>
        /// <typeparam name="T">класса</typeparam>
        /// <param name="file"></param>
        /// <param name="obj"></param>
        public void Save<T>(string file, T obj) where T : class
        {
            File.WriteAllText(_domain + "\\" + file, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// Загрузка объекта
        /// </summary>
        /// <typeparam name="T">класс</typeparam>
        /// <param name="file">файл</param>
        /// <returns></returns>
        public T LoadFromFile<T>(string file) where T : class
        {
            if(File.Exists(_domain + "\\" + file))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(_domain + "\\" + file));
            }
            return null;
        }
    }
}
