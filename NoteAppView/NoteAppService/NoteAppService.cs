using System.ServiceProcess;

namespace NoteAppService
{
    public partial class NoteAppService : ServiceBase
    {
        public NoteAppService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
