using NoteAppModel;
using NoteAppView.Controls;
using System.Windows;
using System.Windows.Controls;

namespace NoteAppView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var authControl = new AuthControl();
            GridMain.Children.Add(authControl);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    Logger.GetInstance().Write("Выбрано : перейти домой");
                    //usc = new UserControlHome();
                    //GridMain.Children.Add(usc);
                    break;
                case "ItemCreate":
                    Logger.GetInstance().Write("Выбрано : создать запись");
                    //usc = new UserControlCreate();
                    //GridMain.Children.Add(usc);
                    break;
                case "ExitApp":
                    Logger.GetInstance().Write("Выбрано : создать запись");
                    System.Windows.Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }
    }
}
