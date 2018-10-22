using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
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
                        }
                    }));
            });
        }

        private void notesListView_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void notesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
