using NoteAppModel;
using NoteAppView.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NoteAppView
{
    /// <summary>
    /// Основной класс окна
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate object MainViewDelegate(MainWindowAction action, object sender, object data);
        public static event MainViewDelegate Event;
        private MainWindowAction _status;

        public MainWindow()
        {
            InitializeComponent();
            if (ViewDataController.GetInstance().UserData == null)
            {
                _status = MainWindowAction.Auth;
                var authControl = new AuthControl();
                GridMain.Children.Add(authControl);
            }
            else
            {
                _status = MainWindowAction.List;
                ShowAllMenu(null);
                ShowData(null);
            }
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
            _status = action;
            switch(action)
            {
                case MainWindowAction.Auth:
                    ShowAuth();
                    return new object();
                case MainWindowAction.Register:
                    ShowRegister(sender);
                    break;
                case MainWindowAction.List:
                    ShowAllMenu(data);
                    ShowData(data);
                    break;
                case MainWindowAction.ChangeNote:
                    GridMain.Children.Clear();
                    Logger.GetInstance().Write("Выбрано : изменить запись");
                    _status = MainWindowAction.ChangeNote;
                    var addNote = new AddNoteControl(((NoteItem)data).Note);
                    GridMain.Children.Add(addNote);
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowData(object data)
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                backButton.Visibility = Visibility.Hidden;
                GridMain.Children.Clear();
                GridMain.Children.Add(new HomeControl());
            }));
        }

        private void ShowAllMenu(object data)
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                FadeTo(itemList);
                FadeTo(newNoteItem);
                itemList.IsEnabled = true;
                newNoteItem.IsEnabled = true;
                userNameText.Text = ViewDataController.GetInstance().UserData.Login;
            }));
        }

        private void FadeTo(ListViewItem control)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = control.Opacity;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(2));
            da.AutoReverse = false;
            control.BeginAnimation(OpacityProperty, da);
        }

        private void ShowRegister(object sender)
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                if (sender is SettingsControl)
                {
                    backButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    backButton.Visibility = Visibility.Visible;
                }
                GridMain.Children.Clear();
                var authControl = new RegisterControl();
                GridMain.Children.Add(authControl);
            }));
        }

        private void ShowAuth()
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                backButton.Visibility = Visibility.Hidden;
                GridMain.Children.Clear();
                var authControl = new AuthControl();
                GridMain.Children.Add(authControl);
            }));
        }

        /// <summary>
        /// Вызов события
        /// </summary>
        /// <param name="action">Действие</param>
        /// <param name="sender">Отправитель</param>
        /// <param name="data">Данные для передачи</param>
        /// <returns></returns>
        public static object InvokeEvent(MainWindowAction action, object sender, object data)
        {
            return Event?.Invoke(action, sender, data);
        }

        #endregion

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridMain.Children.Clear();
            backButton.Visibility = Visibility.Hidden;
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "itemList":
                    Logger.GetInstance().Write("Выбрано : перейти домой");
                    _status = MainWindowAction.List;
                    GridMain.Children.Add(new HomeControl());
                    break;
                case "newNoteItem":
                    Logger.GetInstance().Write("Выбрано : создать запись");
                    _status = MainWindowAction.New;
                    var addNote = new AddNoteControl();
                    GridMain.Children.Add(addNote);
                    break;
                case "settingsItem":
                    Logger.GetInstance().Write("Выбрано : настройки");
                    _status = MainWindowAction.Settings;
                    var settingscontrol = new SettingsControl();
                    GridMain.Children.Add(settingscontrol);
                    break;
                case "ExitApp":
                    Logger.GetInstance().Write("Выбрано : создать запись");
                    System.Windows.Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {

        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridMain.Height = e.NewSize.Height - 140;
        }
    }
}
