using NoteAppModel;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace NoteAppService
{
    public class Client
    {
        public Client(TcpClient client, Logger logger)
        {
            string request = "";
            byte[] buffer = new byte[2048];
            int count;
            while ((count = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);
                if (request.IndexOf("$end") >= 0 || request.Length > 4096)
                {
                    break;
                }
            }
            request = request.Replace("$end", "");
            Match reqMatch = Regex.Match(request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            // Если запрос не удался
            if (reqMatch == Match.Empty)
            {
                // Передаем клиенту ошибку 400 - неверный запрос
                logger.Write("Ошибка в запросе");
                SendError(client, 400);
                return;
            }
            logger.Write("Запрос : " + request);
            
            var commands = request.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var reqs = commands[1].Split(new char[] { ':' });
            var requestHelper = new RequestHelper(logger, reqs[0]);
            if(requestHelper.IsContains())
            {
                request = requestHelper.Execute(Encoding.UTF8.GetString(Convert.FromBase64String(reqs[1])));
                buffer = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(request)));
            }
            else
            {
                SendError(client, 400);
                return;
            }
            string headers = "HTTP/1.1 200 OK\r\nContent-Type: text/json\r\nContent-Length: " + buffer.Length + "\r\nConnection: Keep-Alive" + "\r\n\r\n";
            logger.Write("Ответ на запрос : " + headers + request);
            byte[] headersBuffer = Encoding.ASCII.GetBytes(headers);
            client.GetStream().Write(headersBuffer, 0, headersBuffer.Length);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();
        }

        /// <summary>
        /// Генерация ошибки
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <param name="code">код ошибки</param>
        private void SendError(TcpClient client, int code)
        {
            string codeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            string html = "<html><body><h1>" + codeStr + "</h1></body></html>";
            string str = "HTTP/1.1 " + codeStr + "\r\nContent-type: text/html\r\nContent-Length:" + html.Length.ToString() + "\r\n\r\n" + html;
            byte[] buffer = Encoding.ASCII.GetBytes(str);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();
        }
    }
}
