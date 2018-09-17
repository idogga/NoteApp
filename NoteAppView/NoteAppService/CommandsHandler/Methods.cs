using Newtonsoft.Json;
using NoteAppModel;
using NoteAppModel.DataBase;
using System.Collections.Generic;

namespace NoteAppService
{
    public class Methods
    {
        private Dictionary<string, CommandDelegate> _commands;
        private DataBaseHelper _dbHelper;

        public Methods()
        {
            _dbHelper = new DataBaseHelper();
            _commands =new Dictionary<string, CommandDelegate>();
            _commands.Add("AUTH", Authorize);
            _commands.Add("USERCONTAINS", UserContains);
            _commands.Add("SAVEUSER", SaveUser);
            _commands.Add("GETUSER", GetUser);
            _commands.Add("GETALLNOTES", GetAllNotes);
            _commands.Add("SAVENOTE", SaveNote);
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
            var result = new UserRealm();
            try
            {
                result = _dbHelper.SaveUser(Newtonsoft.Json.JsonConvert.DeserializeObject<UserRealm>(param));
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
            var result = new UserRealm();
            try
            {
                var user = JsonConvert.DeserializeObject<UserRealm>(param);
                result = _dbHelper.GetUser(user.Login, user.Password);
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
        #endregion
    }
}
