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

        public Server(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            Logger.GetInstance().Write("Сервер запущен");
            int MaxThreadsCount = Environment.ProcessorCount * 4;

            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            ThreadPool.SetMinThreads(2, 2);
            while (true)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), _listener.AcceptTcpClient());
                //Task.Factory.StartNew(() =>
                //{
                //    new Client(_listener.AcceptTcpClient(), logger);
                //});
            }
        }

        /// <summary>
        /// Клиентский обработчик
        /// </summary>
        /// <param name="stateInfo"></param>
        private void ClientThread(Object stateInfo)
        {
            new Client((TcpClient)stateInfo);
        }

        public void Dispose()
        {
            if (_listener != null)
            {
                _listener.Stop();
            }
            Logger.GetInstance().Write("Сервер остановлен");
        }
    }
}
