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
        }

        private void AuthBtnClick(object sender, RoutedEventArgs e)
        {
            var user = HttpController.GetInstance().GetUser(loginTextBox.Text, passTextBox.Text);
            if(user == null)
            {
                MessageBox.Show("Неверный логин / пароль", "", MessageBoxButton.OK);
                loginTextBox.Text = "";
                loginTextBox.Text = "";
            }
            else
            {
                ViewDataController.GetInstance().UserData = user;
                Logger.GetInstance().Write("Пользователь : " + user.UserKey + " авторизован");
            }
        }

        private void RegisterBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
