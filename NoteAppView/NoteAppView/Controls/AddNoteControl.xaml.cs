using Microsoft.Win32;
using NoteAppModel.Protocol;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Контрол для создания новой записи
    /// </summary>
    public partial class AddNoteControl : UserControl
    {
        private List<byte[]> _images = new List<byte[]>();
        private NoteAppModel.NoteProtocol newNote;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="oldNote">старая запись</param>
        public AddNoteControl(NoteAppModel.NoteProtocol oldNote = null)
        {
            InitializeComponent();
            if(oldNote!=null)
            {
                FillOldValue(oldNote);
                newNote = oldNote;
            }
            else
            {
                newNote = new NoteAppModel.NoteProtocol();
            }
        }

        private void FillOldValue(NoteAppModel.NoteProtocol oldNote)
        {
            titleTextBox.Text = oldNote.Title;
            contentTextBox.Text = oldNote.ContentText;
            FillCheckBoxes(oldNote.TagsLinks);
        }

        private void FillCheckBoxes(List<int> tagsLinks)
        {
            foreach(var tag in tagsLinks)
            {
                CheckTag(tag);                
            }
        }

        private void CheckTag(int tag)
        {
            foreach (var element in checkBoxGrid.Children)
            {
                if (element is CheckBox checkBox)
                {
                    if(int.Parse(checkBox.Uid) == tag)
                    checkBox.IsChecked = true;
                }
            }
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
            newNote.Title = titleTextBox.Text;
            newNote.ContentText = contentTextBox.Text;
            newNote.TagsLinks = GetTagLinks();
            //newNote.ImageLinks = SaveImages();
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

        private List<int> SaveImages()
        {
            var result = new List<int>();
            foreach (var image in _images)
            {
                var imageProtocol = new ImageLoaderProtocol();
                imageProtocol.ImageSource = image;
                result.Add(HttpController.GetInstance().SaveImage(imageProtocol));
            }
            return result;
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
                _images.Add(ViewDataController.GetInstance().ImageController.ImageToByteArray(image));
            }
        }
    }
}
