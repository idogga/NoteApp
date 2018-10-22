using Newtonsoft.Json;
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
        public int NoteKey { get; set; } 

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст записи
        /// </summary>
        public string ContentText { get; set; }

        /// <summary>
        /// Ссылки на изображения
        /// </summary>
        public IList<int> ImageLinks { get; }

        /// <summary>
        /// Тэги
        /// </summary>
        public IList<int> TagsLinks { get; }

        /// <summary>
        /// Пользовательский ключ
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreateDate { get; set; } 

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// Флаги
        /// </summary>
        public int Flags { get; set; }

        public bool IsFlagOpen(NoteFlagEnum flag)
        {
            return ((int)flag & Flags) == (int)flag;
        }

        public NoteRealm() { }

        public NoteRealm(NoteProtocol protocol)
        {
            NoteKey = protocol.NoteKey;
            ContentText = protocol.ContentText;
            ImageLinks = protocol.ImageLinks;
            TagsLinks = protocol.TagsLinks;
            UserId = protocol.UserId;
            CreateDate = protocol.CreateDate;
            UpdateDate = protocol.UpdateDate;
            Flags = protocol.Flags;
            Title = protocol.Title;
        }
    }
}
