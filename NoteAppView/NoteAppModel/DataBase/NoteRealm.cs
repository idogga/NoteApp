﻿using Newtonsoft.Json;
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
        public IList<int> ImageLinks { get; }

        /// <summary>
        /// Тэги
        /// </summary>
        public IList<int> TagsLinks { get; }

        /// <summary>
        /// Пользовательский ключ
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Флаги
        /// </summary>
        public int Flags { get; set; } = 0;

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
        }
    }
}
