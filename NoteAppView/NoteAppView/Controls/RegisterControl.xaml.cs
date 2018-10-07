using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public RegisterControl()
        {
            InitializeComponent();
            loginTextBox.Focus();
        }

        private void RegBtnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(loginTextBox.Text))
            {
                MessageBox.Show("Введите логин", "", MessageBoxButton.OK);
                loginTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(passTextBox.Password))
            {
                MessageBox.Show("Введите пароль", "", MessageBoxButton.OK);
                passTextBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(confirmPassTextBox.Password))
            {
                MessageBox.Show("Подтвердите пароль", "", MessageBoxButton.OK);
                confirmPassTextBox.Focus();
                return;
            }
            else
            {
                if(confirmPassTextBox.Password != passTextBox.Password)
                {
                    MessageBox.Show("Пароли не совпадают", "", MessageBoxButton.OK);
                    confirmPassTextBox.Password = "";
                    confirmPassTextBox.Focus();
                    return;
                }
                else
                {
                    var user = HttpController.GetInstance().SaveUser(new NoteAppModel.Protocol.UserProtocol()
                    {
                        Login = loginTextBox.Text,
                        Password = passTextBox.Password
                    });
                    if(user == null)
                    {
                        MessageBox.Show("Не удалось создать пользователя, попробуйте еще раз", "", MessageBoxButton.OK);
                        loginTextBox.Text = "";
                        passTextBox.Password = "";
                        confirmPassTextBox.Password = "";
                        loginTextBox.Focus();
                    }
                    else
                    {
                        MessageBox.Show("пользователь успешно создан", "", MessageBoxButton.OK);
                        MainWindow.InvokeEvent(MainWindowAction.List, this, user);
                    }
                }
            }
        }

        private void LoginUnFocus(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            Task.Factory.StartNew(() =>
            {
                if (HttpController.GetInstance().IsContain(login))
                    loginTextBox.Dispatcher.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует", "", MessageBoxButton.OK);
                        loginTextBox.Text = "";
                        loginTextBox.Focus();
                    }));
            });
        }
    }
}
