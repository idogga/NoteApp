using NoteAppModel.Protocol;
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

        public ImageRealm()
        { }

        public ImageRealm(ImageLoaderProtocol protocol)
        {
            ImageKey = protocol.ImageKey;
            ImageSource = protocol.ImageSource;
        }
    }
}
