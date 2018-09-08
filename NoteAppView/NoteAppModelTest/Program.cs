using NoteAppModel;
using NoteAppModel.DataBase;
using System.Diagnostics;

namespace NoteAppModelTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger();
            var helper = new DataBaseHelper(logger);
            var listnotes = helper.GetAllNotes(0);
            if (listnotes != null)
            {
                foreach (var note in listnotes)
                {
                    Debug.WriteLine(note.NoteKey + " | " + note.Title + " | " + note.CreateDate + " | " + note.ContentText);
                }
            }
            else
            {
                Debug.WriteLine("Нет записей");
            }
            helper.SaveNote(new NoteRealm() { Title = "1", UserId = 0, ContentText = "конт1" });
            helper.SaveNote(new NoteRealm() { Title = "2", UserId = 0, ContentText = "конт2" });
            helper.SaveNote(new NoteRealm() { Title = "3", UserId = 1, ContentText = "конт3" });
            helper.SaveNote(new NoteRealm() { Title = "4", UserId = 0, ContentText = "конт4" });
            listnotes = helper.GetAllNotes(0);
            if (listnotes != null)
            {
                foreach (var note in listnotes)
                {
                    Debug.WriteLine(note.NoteKey + " | " + note.Title + " | " + note.CreateDate + " | " + note.ContentText);
                }
            }
            else
            {
                Debug.WriteLine("Нет записей");
            }
        }
    }
}
