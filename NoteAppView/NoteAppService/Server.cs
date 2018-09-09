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
            int MaxThreadsCount = Environment.ProcessorCount * 4;

            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            ThreadPool.SetMinThreads(2, 2);
            while (true)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), _listener.AcceptTcpClient());
                Task.Factory.StartNew(() =>
                {
                    new Client(_listener.AcceptTcpClient(), logger);
                });
            }
        }
        private void ClientThread(Object stateInfo)
        {
            new Client((TcpClient)stateInfo, _logger);
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
