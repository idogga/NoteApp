using NoteAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Logger logger = new Logger();
            logger.Write("Служба запущена");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new NoteAppService()
            };
            ServiceBase.Run(ServicesToRun);
            new Server(logger, 1333);
        }
    }
}
