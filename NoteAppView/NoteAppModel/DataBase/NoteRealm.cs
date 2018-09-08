using Realms;
using System;
using System.Collections.Generic;

namespace NoteAppModel.DataBase
{
    public class NoteRealm : RealmObject
    {
        /// <summary>
        /// Первичный ключ для записи
        /// </summary>
        [PrimaryKey]
        public int NoteKey { get; set; } = 0;

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Текст записи
        /// </summary>
        public string ContentText { get; set; } = string.Empty;

        /// <summary>
        /// Ссылки на изображения
        /// </summary>
        public IList<int> ImagLinks { get; }

        /// <summary>
        /// Тэги
        /// </summary>
        public IList<int> TagsLinks { get; }

        public int UserId { get; set; } = 0;
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.Now;
    }
}
