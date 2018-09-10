﻿using Realms;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NoteAppModel.DataBase
{
    public class NoteRealm : RealmObject
    {
        /// <summary>
        /// Первичный ключ для записи
        /// </summary>
        [PrimaryKey]
        [JsonProperty("key")]
        public int NoteKey { get; set; } = 0;

        /// <summary>
        /// Заголовок
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Текст записи
        /// </summary>
        [JsonProperty("content")]
        public string ContentText { get; set; } = string.Empty;

        /// <summary>
        /// Ссылки на изображения
        /// </summary>
        [JsonProperty("iLink")]
        public IList<int> ImageLinks { get; }

        /// <summary>
        /// Тэги
        /// </summary>
        [JsonProperty("tLink")]
        public IList<int> TagsLinks { get; }

        /// <summary>
        /// Пользовательский ключ
        /// </summary>
        [JsonProperty("User")]
        public int UserId { get; set; } = 0;

        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("creteAt")]
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Дата изменения
        /// </summary>
        [JsonProperty("updateAt")]
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Флаги
        /// </summary>
        [JsonProperty("flags")]
        public int Flags { get; set; } = 0;

        public bool IsFlagOpen(NoteFlagEnum flag)
        {
            return ((int)flag & Flags) == (int)flag;
        }
    }
}
