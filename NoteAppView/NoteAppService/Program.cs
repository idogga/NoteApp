using NoteAppModel;

namespace NoteAppService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Logger.GetInstance().Write("Служба запущена");
#if RELEASE
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new NoteAppService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
            new Server(1333);
        }
    }
}
