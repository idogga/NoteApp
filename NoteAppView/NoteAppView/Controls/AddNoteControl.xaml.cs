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

        private void AcceptClicked(object sender, RoutedEventArgs e)
        {
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
