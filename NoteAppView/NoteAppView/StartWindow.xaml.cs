using NoteAppModel;
using System.Windows;

namespace NoteAppView
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            InitializeHelpers();
            Logger.GetInstance().Write("Загрузка формы прошла успешно!");
        }

        /// <summary>
        /// Инициализация приватных классов
        /// </summary>
        private void InitializeHelpers()
        {
        }
    }
}
