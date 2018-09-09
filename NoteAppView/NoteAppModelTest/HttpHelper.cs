using NoteAppModel;
using NoteAppModel.DataBase;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NoteAppModelTest
{
    public class HttpHelper
    {
        private readonly HttpClient client = new HttpClient();
        private Logger _logger;

        public HttpHelper(Logger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Имеется ли пользователем с таким логином
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool IsContain(string login)
        {
            try
            {
                var answer = MakeRequest("USERCONTAINS", login);
                return bool.Parse(answer);
            }
            catch (Exception ex)
            {
                _logger.Write(ex);
                return true;
            }
        }

        /// <summary>
        /// Сохранение пользователя
        /// </summary>
        /// <param name="result"></param>
        internal bool SaveUser(UserRealm result)
        {
            try
            {
                var answer = MakeRequest("SaveUser", JsonConvert.SerializeObject(result));
                return bool.Parse(answer);
            }
            catch (Exception ex)
            {
                _logger.Write(ex);
                return true;
            }
        }

        internal bool SaveNote(NoteRealm newNote)
        {
            try
            {
                var answer = MakeRequest("SaveNote", JsonConvert.SerializeObject(newNote));
                return bool.Parse(answer);
            }
            catch (Exception ex)
            {
                _logger.Write(ex);
                return true;
            }
        }

        internal UserRealm GetUser(string login, string pass)
        {
            UserRealm user = new UserRealm() { Login = login, Password = pass };
            try
            {
                var str = MakeRequest("getUser", JsonConvert.SerializeObject(user));
                return JsonConvert.DeserializeObject<UserRealm>(str);
            }
            catch
            {
                user = null;
            }
            return user;
        }

        internal List<NoteRealm> GetAllNotes(int userKey)
        {
            List<NoteRealm> result = new List<NoteRealm>();
            try
            {
                var str = MakeRequest("GetAllNotes", JsonConvert.SerializeObject(userKey));
                return JsonConvert.DeserializeObject<List<NoteRealm>>(str);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        #region Приватные методы
        private string MakeRequest(string name, string body)
        {
            var bodyByte = Encoding.UTF8.GetBytes(name + ":" + Convert.ToBase64String(Encoding.UTF8.GetBytes(body)) + "$end");
            var request = WebRequest.Create("http://localhost:1333");
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "text/json";
            request.ContentLength = bodyByte.Length;
            request.Timeout = 60000;
            _logger.Write("Запрос отправлен : " + body);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bodyByte, 0, bodyByte.Length);
            }
            using (var response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                    _logger.Write("Ответ получен : " + responseString);
                    return responseString;
                }
            }
        }
        #endregion

    }
}
