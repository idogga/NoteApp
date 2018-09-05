using NoteAppModel;
using System.Windows;

namespace NoteAppView
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private Logger _logger;

        public StartWindow()
        {
            InitializeComponent();
            InitializeHelpers();
            _logger.Write("Загрузка формы прошла успешно!");
        }

        /// <summary>
        /// Инициализация приватных классов
        /// </summary>
        private void InitializeHelpers()
        {
            _logger = new Logger();
        }
    }
}
