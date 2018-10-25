using System.Windows.Controls;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void ReAuthClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.InvokeEvent(MainWindowAction.Auth, this, null);
        }

        private void RegisterClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewDataController.GetInstance().UserData.Remove();
            MainWindow.InvokeEvent(MainWindowAction.Register, this, null);
        }
    }
}
