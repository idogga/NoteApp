using Newtonsoft.Json;
using NoteAppModel;
using NoteAppModel.DataBase;
using NoteAppModel.Protocol;
using System.Collections.Generic;

namespace NoteAppService
{
    /// <summary>
    /// Методы для выполнения
    /// </summary>
    public class Methods
    {
        private Dictionary<string, CommandDelegate> _commands;
        private DataBaseHelper _dbHelper;

        public Methods()
        {
            try
            {
                _dbHelper = new DataBaseHelper();
            }
            catch { }
            _commands =new Dictionary<string, CommandDelegate>();
            _commands.Add("AUTH", Authorize);
            _commands.Add("USERCONTAINS", UserContains);
            _commands.Add("SAVEUSER", SaveUser);
            _commands.Add("GETUSER", GetUser);
            _commands.Add("GETALLNOTES", GetAllNotes);
            _commands.Add("SAVENOTE", SaveNote);
            _commands.Add("SAVEIMAGE", SaveImage);
            _commands.Add("LOADIMAGE", LoadImage);

        }
        
        /// <summary>
        /// Словарь со всеми командами
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CommandDelegate> GetAvailableCommands()
        {
            return _commands;
        }

        #region Вспомогательные методы
        /// <summary>
        /// Сериализация объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        #endregion

        #region Функции-команды
        
        /// <summary>
        /// Автоирзация пользователя
        /// </summary>
        /// <param name="param">логин,пароль</param>
        /// <returns>пользователь</returns>
        private string Authorize(string param)
        {
            var user = new NoteAppModel.DataBase.NoteRealm();
            return Serialize(user);
        }
        
        /// <summary>
        /// Есть ли пользователь с таким логином в БД
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string UserContains(string param)
        {
            var result = _dbHelper.UserContains(param);
            return Serialize(result);
        }

        /// <summary>
        /// Сохранение данных пользователя
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string SaveUser(string param)
        {
            var result = new UserProtocol();
            try
            {
                result = new UserProtocol(_dbHelper.SaveUser(new UserRealm(Newtonsoft.Json.JsonConvert.DeserializeObject<UserProtocol>(param))));
            }
            catch
            {
                result = null;
            }
            return Serialize(result);
        }

        /// <summary>
        /// поиск пользователя
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetUser(string param)
        {
            var result = new UserProtocol();
            try
            {
                var user = JsonConvert.DeserializeObject<UserProtocol>(param);
                result = new UserProtocol(_dbHelper.GetUser(user.Login, user.Password));
            }
            catch
            {
                result = null;
            }
            return Serialize(result);
        }

        /// <summary>
        /// Получить все записи пользователя
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GetAllNotes(string param)
        {
            var result = new List<NoteProtocol>();
            try
            {
                var user = JsonConvert.DeserializeObject<int>(param);
                foreach (var note in _dbHelper.GetAllNotes(user))
                    result.Add(new NoteProtocol(note));
            }
            catch
            {
                result = null;
            }
            return Serialize(result);
        }

        /// <summary>
        /// Получить все записи пользователя
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string SaveNote(string param)
        {
            var result = false;
            try
            {
                _dbHelper.SaveNote(new NoteRealm(JsonConvert.DeserializeObject<NoteProtocol>(param)));
                result = true;
            }
            catch
            {
                result = false;
            }
            return Serialize(result);
        }

        /// <summary>
        /// Сохранение картинки
        /// </summary>
        /// <param name="inRequest"></param>
        /// <returns></returns>
        private string SaveImage(string inRequest)
        {
            var result = 0;
            try
            {
                result = _dbHelper.SaveImage(new ImageRealm(JsonConvert.DeserializeObject<ImageLoaderProtocol>(inRequest)));
            }
            catch
            {
                result = 0;
            }
            return Serialize(result);
        }

        /// <summary>
        /// Загрузка картинки
        /// </summary>
        /// <param name="inRequest"></param>
        /// <returns></returns>
        private string LoadImage(string inRequest)
        {
            ImageLoaderProtocol result = null;
            try
            {
                result = new ImageLoaderProtocol(_dbHelper.GetImage(JsonConvert.DeserializeObject<int>(inRequest)));
            }
            catch
            {
                result = null;
            }
            return Serialize(result);
        }
        #endregion
    }
}
