using Newtonsoft.Json;
using NoteAppModel;
using NoteAppModel.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NoteAppView
{
    public class HttpController
    {
        private readonly HttpClient client = new HttpClient();

        public HttpController()
        {
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
                Logger.GetInstance().Write(ex);
                return true;
            }
        }

        /// <summary>
        /// Сохранение пользователя
        /// </summary>
        /// <param name="result"></param>
        internal UserProtocol SaveUser(UserProtocol result)
        {
            try
            {
                var answer = MakeRequest("SaveUser", JsonConvert.SerializeObject(result));
                return JsonConvert.DeserializeObject <UserProtocol> (answer);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Write(ex);
                return null;
            }
        }

        internal bool SaveNote(NoteProtocol newNote)
        {
            try
            {
                var answer = MakeRequest("SaveNote", JsonConvert.SerializeObject(newNote));
                return bool.Parse(answer);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().Write(ex);
                return true;
            }
        }

        internal UserProtocol GetUser(string login, string pass)
        {
            var user = new UserProtocol() { Login = login, Password = pass };
            try
            {
                var str = MakeRequest("getUser", JsonConvert.SerializeObject(user));
                return JsonConvert.DeserializeObject<UserProtocol>(str);
            }
            catch
            {
                user = null;
            }
            return user;
        }

        internal List<NoteProtocol> GetAllNotes(int userKey)
        {
            List<NoteProtocol> result = new List<NoteProtocol>();
            try
            {
                var str = MakeRequest("GetAllNotes", JsonConvert.SerializeObject(userKey));
                return JsonConvert.DeserializeObject<List<NoteProtocol>>(str);
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
            Logger.GetInstance().Write("Запрос отправлен : " + body);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bodyByte, 0, bodyByte.Length);
            }
            using (var response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseString = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                    Logger.GetInstance().Write("Ответ получен : " + responseString);
                    return responseString;
                }
            }
        }
        #endregion

    }
}
