using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        private bool _isFirstSelection = true;
        #region private
        private List<NoteAppModel.NoteProtocol> _notes;
        private ObservableCollection<NoteItem> _noteItems = new ObservableCollection<NoteItem>();
        #endregion
        public HomeControl()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Task.Factory.StartNew(() =>
            {
                _notes = HttpController.GetInstance().GetAllNotes(ViewDataController.GetInstance().UserData.UserKey);
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (_notes == null || _notes.Count == 0)
                        {
                            notNote.Visibility = Visibility.Visible;
                            notesListView.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            _noteItems.Clear();
                            foreach (var note in _notes)
                            {
                                _noteItems.Add(new NoteItem(note));
                            }
                            notesListView.ItemsSource = _noteItems;
                            notesListView.SelectedItem = null;
                        }
                    }));
            });
        }

        private void notesListView_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void notesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null)
                return;
            if (e.AddedItems.Count == 0)
                return;
            if (_isFirstSelection)
            {
                _isFirstSelection = false;
            }
            else
            {
                MainWindow.InvokeEvent(MainWindowAction.ChangeNote, this, e.AddedItems[0]);
            }
        }
    }
}
