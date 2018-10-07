﻿using NoteAppModel;
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
        List,
    }

    public partial class MainWindow : Window
    {
        public delegate object MainViewDelegate(MainWindowAction action, object sender, object data);
        public static event MainViewDelegate Event;
        private MainWindowAction _status;

        public MainWindow()
        {
            InitializeComponent();
            _status = MainWindowAction.Auth;
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
            _status = action;
            switch(action)
            {
                case MainWindowAction.Auth:
                    ShowAuth();
                    return new object();
                case MainWindowAction.Register:
                    ShowRegister();
                    break;
                case MainWindowAction.List:
                    ShowAllMenu(data);
                    ShowData(data);
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

            }));
        }

        private void ShowAllMenu(object data)
        {
            
        }

        private void ShowRegister()
        {
            GridMain.Dispatcher.Invoke(new Action(() =>
            {
                backButton.Visibility = Visibility.Visible;
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

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
