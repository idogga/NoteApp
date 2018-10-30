using NoteAppModel;

namespace NoteAppView.Controls
{
    /// <summary>
    /// Заметка пользователя
    /// </summary>
    public class NoteItem
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public string DateCreature { get; set; }
        /// <summary>
        /// Тэги
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Заметка
        /// </summary>
        public NoteAppModel.NoteProtocol Note;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="note"></param>
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
