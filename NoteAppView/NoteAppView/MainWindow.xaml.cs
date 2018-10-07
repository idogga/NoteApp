using NoteAppModel;
using NoteAppView.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NoteAppView
{
    public enum MainWindowAction
    {
        Auth,
        Register,

    }



    public partial class MainWindow : Window
    {
        public delegate object MainViewDelegate(MainWindowAction action, object sender, object data);
        public static event MainViewDelegate Event;

        public MainWindow()
        {
            InitializeComponent();
            var authControl = new AuthControl();
            GridMain.Children.Add(authControl);
            Event += MainWindow_Event;
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

        #region static
        private object MainWindow_Event(MainWindowAction action, object sender, object data)
        {
            switch(action)
            {
                case MainWindowAction.Auth:
                    ShowAuth();
                    return new object();
                case MainWindowAction.Register:
                    ShowRegister();
                    break;
                default:
                    break;
            }
            return null;
        }

        private void ShowRegister()
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                GridMain.Children.Clear();
                var authControl = new RegisterControl();
                GridMain.Children.Add(authControl);
            }));
        }

        private void ShowAuth()
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                GridMain.Children.Clear();
                var authControl = new AuthControl();
                GridMain.Children.Add(authControl);
            }));
        }

        public static object InvokeEvent(MainWindowAction action, object sender, object data)
        {
            return Event?.Invoke(action, sender, data);
        }

        #endregion

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
