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
        public int NoteKey { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Текст записи
        /// </summary>
        [JsonProperty("content")]
        public string ContentText { get; set; } 

        /// <summary>
        /// Ссылки на изображения
        /// </summary>
        [JsonProperty("iLink")]
        public List<int> ImageLinks { get; set; }

        /// <summary>
        /// Тэги
        /// </summary>
        [JsonProperty("tLink")]
        public List<int> TagsLinks { get; set; }

        /// <summary>
        /// Пользовательский ключ
        /// </summary>
        [JsonProperty("User")]
        public int UserId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("creteAt")]
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        [JsonProperty("updateAt")]
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// Флаги
        /// </summary>
        [JsonProperty("flags")]
        public int Flags { get; set; }

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
            Title = realm.Title;
        }
    }
}
