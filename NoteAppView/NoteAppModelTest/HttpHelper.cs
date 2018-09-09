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
            var bodyByte = Encoding.UTF8.GetBytes("USERCONTAINS:" + login + "$end");
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:1333");
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "text/json";
            request.ContentLength = bodyByte.Length;
            request.KeepAlive = true;
            request.Timeout = 600000;
            request.ServicePoint.Expect100Continue = false;
            request.ServicePoint.MaxIdleTime = 0;
            request.ProtocolVersion = HttpVersion.Version10;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bodyByte, 0, bodyByte.Length);
            }
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    _logger.Write("response");
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = reader.ReadToEnd();
                        _logger.Write("Ответ получен : " + responseString);
                        return bool.Parse(responseString);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.Write(ex);
                return true;
            }
        }
    }
}
