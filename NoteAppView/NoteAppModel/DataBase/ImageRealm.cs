using Realms;

namespace NoteAppModel.DataBase
{
    public class ImageRealm : RealmObject
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [PrimaryKey]
        public int ImageKey { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        public byte[] ImageSource { get; set; }
    }
}
