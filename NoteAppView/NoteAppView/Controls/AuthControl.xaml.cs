using NoteAppModel;
using System.Windows;
using System.Windows.Controls;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Interaction logic for AuthControl.xaml
    /// </summary>
    public partial class AuthControl : UserControl
    {
        public AuthControl()
        {
            InitializeComponent();
            loginTextBox.Focus();
        }

        private void AuthBtnClick(object sender, RoutedEventArgs e)
        {
            var user = HttpController.GetInstance().GetUser(loginTextBox.Text, passTextBox.Password);
            if(user == null)
            {
                MessageBox.Show("Неверный логин / пароль", "", MessageBoxButton.OK);
                loginTextBox.Text = "";
                passTextBox.Password = "";
            }
            else
            {
                ViewDataController.GetInstance().UserData = user;
                Logger.GetInstance().Write("Пользователь : " + user.UserKey + " авторизован");
                MainWindow.InvokeEvent(MainWindowAction.List, this, user);
            }
        }

        private void RegisterBtnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.InvokeEvent(MainWindowAction.Register, this, null);
        }

        private void AdditionalButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная функция находится в разработке");
        }
    }
}
