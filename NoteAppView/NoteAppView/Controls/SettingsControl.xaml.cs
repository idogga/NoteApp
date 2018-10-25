using System.Linq;
using System.Net;
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

        private void addressTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == null)
            {
                acceptButton.IsEnabled = false;
                return;
            }
            if(sender is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    acceptButton.IsEnabled = false;
                    return;
                }
                if (textBox.Text.Count(c => c == '.') != 3)
                {
                    acceptButton.IsEnabled = false;
                    return;
                }
                IPAddress addr;
                if (IPAddress.TryParse(textBox.Text, out addr))
                {
                    acceptButton.IsEnabled = true;
                }
                else
                {
                    acceptButton.IsEnabled = false;
                }
            }
            else
            {
                acceptButton.IsEnabled = false;
            }
        }

        private void AcceptClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            HttpController.GetInstance().Address = addressTextBox.Text;
        }
    }
}
