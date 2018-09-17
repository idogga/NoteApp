using Newtonsoft.Json;
using NoteAppModel.DataBase;
using NoteAppModel.Protocol;
using System;
using System.Collections.Generic;

namespace NoteAppModel
{
    public class NoteProtocol : RequestProtocol
    {
        /// <summary>
        /// Первичный ключ для записи
        /// </summary>
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
        public List<int> ImageLinks { get; set; } = new List<int>();

        /// <summary>
        /// Тэги
        /// </summary>
        [JsonProperty("tLink")]
        public List<int> TagsLinks { get; set; } = new List<int>();

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

        public NoteProtocol() { }

        public NoteProtocol(NoteRealm realm)
        {
            NoteKey = realm.NoteKey;
            ContentText = realm.ContentText;
            ImageLinks = new List<int>(realm.ImageLinks);
            TagsLinks = new List<int>(realm.TagsLinks);
            UserId = realm.UserId;
            CreateDate = realm.CreateDate;
            UpdateDate = realm.UpdateDate;
            Flags = realm.Flags;
        }
    }
}
