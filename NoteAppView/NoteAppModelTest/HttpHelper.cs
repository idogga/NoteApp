using NoteAppModel;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

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
    }
}
