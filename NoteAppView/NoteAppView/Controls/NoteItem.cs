using NoteAppModel;

namespace NoteAppView.Controls
{
    public class NoteItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreature { get; set; }
        public string Tags { get; set; }
        public NoteAppModel.NoteProtocol Note;

        public NoteItem(NoteAppModel.NoteProtocol note)
        {
            Note = note;
            Title = note.Title;
            Description = note.ContentText;
            DateCreature = note.CreateDate.ToLocalTime().ToString("HH:mm:ss dd.MM.yyyy");
            Tags = (new NoteTypes()).GetTypesString(note.TagsLinks);
        }
    }
}
