using System.Windows;
using System.Windows.Controls;

namespace NoteAppView.Controls
{
    public partial class AddNoteControl : UserControl
    {
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
            return true;
        }

        private void AcceptClicked(object sender, RoutedEventArgs e)
        {
            if (!CheckNote()) return;
            
            var newNote = new NoteAppModel.NoteProtocol();
            newNote.Title = titleTextBox.Text;
            newNote.ContentText = contentTextBox.Text;
            newNote.TagsLinks = new System.Collections.Generic.List<int>() { 1, 2 };
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

        private void ClearClicked(object sender, RoutedEventArgs e)
        {
            titleTextBox.Text = "";
            contentTextBox.Text = "";
        }
    }
}
