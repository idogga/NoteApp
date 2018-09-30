using NoteAppModel;
using System.Windows;

namespace NoteAppView
{
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
            Logger.GetInstance().Write("Загрузка формы прошла успешно!");
        }
    }
}
