using NoteAppModel;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace NoteAppService
{
    public class Server : IDisposable
    {
        private TcpListener _listener;
        private Logger _logger;

        public Server(Logger logger, int port)
        {
            _logger = logger;
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _logger.Write("Сервер запущен");
            while (true)
            {
                var token = new CancellationToken();
                Task.Factory.StartNew(async x =>
                {
                    _logger.Write("Получен запрос");
                    var client = _listener.AcceptTcpClient();
                    var client1 = new Client(client, logger);
                }, token);
            }
        }

        public void Dispose()
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
            _logger.Write("Сервер остановлен");
        }
    }
}
