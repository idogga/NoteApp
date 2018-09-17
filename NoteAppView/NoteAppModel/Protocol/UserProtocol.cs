using Newtonsoft.Json;
using NoteAppModel.DataBase;
using System;

namespace NoteAppModel.Protocol
{
    public class UserProtocol : RequestProtocol
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [JsonProperty("key")]
        public int UserKey { get; set; } = 0;

        /// <summary>
        /// Логин
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        [JsonProperty("pass")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("createAt")]
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Дата изменения профиля
        /// </summary>
        [JsonProperty("updateAt")]
        public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// Флаги
        /// </summary>
        [JsonProperty("flags")]
        public int Flags { get; set; } = 0;

        public UserProtocol() { }

        public UserProtocol(UserRealm realm)
        {
            UserKey = realm.UserKey;
            Login = realm.Login;
            Password = realm.Password;
            CreateDate = realm.CreateDate;
            UpdateDate = realm.UpdateDate;
            Flags = realm.Flags;
        }
    }
}
