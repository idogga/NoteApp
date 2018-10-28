using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NoteAppView.Controls
{
    public partial class AddNoteControl : UserControl
    {
        List<byte[]> _images=new List<byte[]>();

        public AddNoteControl(NoteAppModel.NoteProtocol oldNote = null)
        {
            InitializeComponent();
            if(oldNote!=null)
            {
                FillOldValue(oldNote);
            }
        }

        private void FillOldValue(NoteAppModel.NoteProtocol oldNote)
        {
            titleTextBox.Text = oldNote.Title;
            contentTextBox.Text = oldNote.ContentText;
        }

        /// <summary>
        /// проверка заметки на корректный ввод
        /// </summary>
        /// <returns></returns>
        private bool CheckNote()
        {
            if(string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Введите название", "", MessageBoxButton.OK);
                titleTextBox.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(contentTextBox.Text))
            {
                MessageBox.Show("Введите саму заметку", "", MessageBoxButton.OK);
                contentTextBox.Focus();
                return false;
            }
            var isCheckBoxSelect = false;
            foreach (var element in checkBoxGrid.Children)
            {
                if (element is CheckBox checkBox)
                {
                    bool ischeck = checkBox.IsChecked ?? false;
                    isCheckBoxSelect |= ischeck; 
                }
            }
            if (!isCheckBoxSelect)
            {
                MessageBox.Show("Выберите хотя бы одну тему", "", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        private void AcceptClicked(object sender, RoutedEventArgs e)
        {
            if (!CheckNote())
                return;
            var newNote = new NoteAppModel.NoteProtocol();
            newNote.Title = titleTextBox.Text;
            newNote.ContentText = contentTextBox.Text;
            newNote.TagsLinks = GetTagLinks();
            newNote.UserId = ViewDataController.GetInstance().UserData.UserKey;
            if (HttpController.GetInstance().SaveNote(newNote))
            {
                MainWindow.InvokeEvent(MainWindowAction.List, this, newNote);
            }
            else
            {
                MessageBox.Show("Не удалось сохранить запись", "", MessageBoxButton.OK);
            }
        }

        private List<int> GetTagLinks()
        {
            var result = new List<int>();
            foreach (var element in checkBoxGrid.Children)
            {
                if (element is CheckBox check)
                {
                    if (check.IsChecked ?? false)
                        result.Add(int.Parse(check.Uid));
                }
            }
            return result;
        }

        private void ClearClicked(object sender, RoutedEventArgs e)
        {
            titleTextBox.Text = "";
            contentTextBox.Text = "";
            foreach(var element in checkBoxGrid.Children)
            {
                if(element is CheckBox checkBox)
                {
                    checkBox.IsChecked = false;
                }
            }
            _images.Clear();
        }

        private void AddPictureClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var image = new BitmapImage(new Uri(op.FileName));
                _images.Add(ViewDataController.GetInstance().ImageController.ImageToByteArray(new System.Drawing.Bitmap(image.StreamSource)));
            }
        }
    }
}
