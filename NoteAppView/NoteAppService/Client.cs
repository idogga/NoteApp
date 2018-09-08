using NoteAppModel;
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
            byte[] buffer = new byte[1024];
            int count;
            while ((count = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);
                if (request.IndexOf("\r\n\r\n") >= 0 || request.Length > 4096)
                {
                    break;
                }
            }
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

            string RequestUri = reqMatch.Groups[1].Value;
            string html = "<html><body><h1>It works!</h1></body></html>";
            buffer = Encoding.ASCII.GetBytes(html);
            string headers = "HTTP/1.1 200 OK\nContent-Type: " + "text/html" + "\nContent-Length: " + buffer.Length + "\n\n";
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
            string str = "HTTP/1.1 " + codeStr + "\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;
            byte[] buffer = Encoding.ASCII.GetBytes(str);
            client.GetStream().Write(buffer, 0, buffer.Length);
            client.Close();
        }
    }
}
